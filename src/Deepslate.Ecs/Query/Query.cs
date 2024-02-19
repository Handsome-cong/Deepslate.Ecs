﻿using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed class Query
{
    private Archetype[] _matchedArchetypes = Array.Empty<Archetype>();
    private readonly Type[] _requiredWritableComponentTypes;
    private readonly Type[] _requiredReadOnlyComponentTypes;
    private readonly Type[] _includedComponentTypes;
    private readonly Type[] _excludedComponentTypes;
    private readonly Predicate<Archetype>? _filter;

    private UsageCode[] _usageCodes = Array.Empty<UsageCode>();

    private FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>> _storages =
        FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>>.Empty;

    internal ReadOnlySpan<UsageCode> UsageCodes => _usageCodes;

    /// <summary>
    /// Empty until <see cref="WorldBuilder.Build"/> is called.
    /// </summary>
    public IReadOnlyList<Archetype> MatchedArchetypes => _matchedArchetypes;

    /// <summary>
    /// Empty until <see cref="WorldBuilder.Build"/> is called.
    /// </summary>
    public FrozenSet<Archetype> MatchedArchetypesSet { get; private set; } = FrozenSet<Archetype>.Empty;

    public IEnumerable<Type> RequiredWritableComponentTypes => _requiredWritableComponentTypes;
    public IEnumerable<Type> RequiredReadOnlyComponentTypes => _requiredReadOnlyComponentTypes;
    public bool RequireInstantArchetypeCommand { get; }

    internal Query(
        IEnumerable<Type> requiredWritableComponentTypes,
        IEnumerable<Type> requiredReadOnlyComponentTypes,
        IEnumerable<Type> includedComponentTypes,
        IEnumerable<Type> excludedComponentTypes,
        Predicate<Archetype>? filter,
        bool requireInstantArchetypeCommand)
    {
        _requiredWritableComponentTypes = requiredWritableComponentTypes.ToArray();
        _requiredReadOnlyComponentTypes = requiredReadOnlyComponentTypes.ToArray();
        _includedComponentTypes = includedComponentTypes.ToArray();
        _excludedComponentTypes = excludedComponentTypes.ToArray();
        _filter = filter;
        RequireInstantArchetypeCommand = requireInstantArchetypeCommand;
    }

    internal void PostInitialize(World world)
    {
        _matchedArchetypes = GetMatchedArchetypes(world.Archetypes, world.ComponentTypeToArchetypeIds);
        MatchedArchetypesSet = _matchedArchetypes.ToFrozenSet();
        _usageCodes = CalculateUsageCodes(world.ComponentTypeIds, world.Archetypes.Count);

        _storages = CollectStorages(
            _requiredWritableComponentTypes.Concat(_requiredReadOnlyComponentTypes),
            world.StorageArrayFactory);
    }

    private FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>> CollectStorages(
        IEnumerable<Type> componentTypes,
        StorageArrayFactory storageArrayFactory)
    {
        return componentTypes.ToFrozenDictionary(
            componentType => componentType,
            componentType =>
            {
                var storages =
                    _matchedArchetypes.Select(archetype => archetype.ComponentStorageDictionary[componentType]);
                return storageArrayFactory.Factories[componentType](storages);
            });
    }

    internal IComponentDataStorage<TComponent>[] GetStorages<TComponent>()
        where TComponent : IComponentData
    {
        _storages.TryGetValue(typeof(TComponent), out var storages);
        if (storages is IComponentDataStorage<TComponent>[] typedStorages)
        {
            return typedStorages;
        }

        throw new ArgumentOutOfRangeException(nameof(TComponent), typeof(TComponent),
            "No storages found for the given type.");
    }

    private Archetype[] GetMatchedArchetypes(
        IReadOnlyList<Archetype> archetypes,
        FrozenDictionary<Type, FrozenSet<ushort>> componentTypeToArchetypeIds)
    {
        var counters = new int[archetypes.Count];

        foreach (var componentType in _includedComponentTypes)
        {
            var indices = componentTypeToArchetypeIds[componentType];
            foreach (var index in indices)
            {
                counters[index]++;
            }
        }

        foreach (var componentType in _excludedComponentTypes)
        {
            var indices = componentTypeToArchetypeIds[componentType];
            foreach (var index in indices)
            {
                counters[index]--;
            }
        }

        var requiredComponentTypeCount = _includedComponentTypes.Length;
        List<int> matchedIndices = [];
        for (var index = 0; index < counters.Length; index++)
        {
            if (counters[index] == requiredComponentTypeCount && (_filter?.Invoke(archetypes[index]) ?? true))
            {
                matchedIndices.Add(index);
            }
        }

        var result = new Archetype[matchedIndices.Count];
        for (var index = 0; index < matchedIndices.Count; index++)
        {
            result[index] = archetypes[matchedIndices[index]];
        }

        return result;
    }

    private UsageCode[] CalculateUsageCodes(FrozenDictionary<Type, int> componentTypeIds, int allArchetypeCount)
    {
        var allComponentTypeCount = componentTypeIds.Count;
        var archetypeUsageCodeCount = (allArchetypeCount - 1) / UsageCode.SizeOfBits + 1;
        var componentUsageCodeCount = (allComponentTypeCount - 1) / UsageCode.SizeOfBits + 1;
        var queryUsageCodeCount = archetypeUsageCodeCount + componentUsageCodeCount * 2;
        var queryUsageCode = new UsageCode[queryUsageCodeCount];
        var queryUsageCodeSpan = queryUsageCode.AsSpan();
        var archetypeUsageCodes = queryUsageCodeSpan[..archetypeUsageCodeCount];
        var writableComponentUsageCode = queryUsageCodeSpan[
            archetypeUsageCodeCount..(archetypeUsageCodeCount + componentUsageCodeCount)];
        var readableComponentUsageCode = queryUsageCodeSpan[(archetypeUsageCodeCount + componentUsageCodeCount)..];
        foreach (var archetype in MatchedArchetypes)
        {
            var archetypeId = archetype.Id;
            ref var archetypesUsageCode = ref archetypeUsageCodes[archetypeId / UsageCode.SizeOfBits];
            archetypesUsageCode = archetypesUsageCode.WithBitOffset(archetypeId % UsageCode.SizeOfBits);
        }

        FillComponentUsageCode(writableComponentUsageCode, _requiredWritableComponentTypes, componentTypeIds);
        FillComponentUsageCode(readableComponentUsageCode, _requiredWritableComponentTypes, componentTypeIds);
        FillComponentUsageCode(readableComponentUsageCode, _requiredReadOnlyComponentTypes, componentTypeIds);
        
        FillComponentUsageCode(readableComponentUsageCode, [typeof(InstantCommandComponent)], componentTypeIds);

        if (RequireInstantArchetypeCommand)
        {
            var allComponentTypes = MatchedArchetypes
                .SelectMany(archetype => archetype.ComponentTypes)
                .ToArray();
            FillComponentUsageCode(writableComponentUsageCode, allComponentTypes, componentTypeIds);
            FillComponentUsageCode(readableComponentUsageCode, allComponentTypes, componentTypeIds);

            FillComponentUsageCode(writableComponentUsageCode, [typeof(InstantCommandComponent)], componentTypeIds);
        }

        return queryUsageCode;
    }

    private static void FillComponentUsageCode(
        Span<UsageCode> componentUsageCodes,
        Span<Type> componentTypes,
        IReadOnlyDictionary<Type, int> componentTypeIds)
    {
        foreach (var componentType in componentTypes)
        {
            var componentTypeId = componentTypeIds[componentType];
            ref var componentUsageCode = ref componentUsageCodes[componentTypeId / UsageCode.SizeOfBits];
            componentUsageCode = componentUsageCode.WithBitOffset(componentTypeId % UsageCode.SizeOfBits);
        }
    }
}