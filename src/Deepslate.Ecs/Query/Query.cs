using System.Collections.Frozen;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

public sealed class Query
{
    private readonly Archetype[] _matchedArchetypes;
    private readonly Type[] _requiredWritableComponentTypes;
    private readonly Type[] _requiredReadOnlyComponentTypes;

    private readonly FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>> _writableComponentStorages;
    private readonly FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>> _readOnlyComponentStorages;
    private readonly FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>> _storages;

    public IEnumerable<Archetype> MatchedArchetypes => _matchedArchetypes;
    public IEnumerable<Type> RequiredWritableComponentTypes => _requiredWritableComponentTypes;
    public IEnumerable<Type> RequiredReadOnlyComponentTypes => _requiredReadOnlyComponentTypes;
    public ArchetypeCommandType ArchetypeCommandType { get; private set; }
    public bool RequireInstantArchetypeCommand { get; }

    public int Count => _matchedArchetypes.Sum(archetype => archetype.Count);


    internal Query(
        IEnumerable<Archetype> matchedArchetypes,
        IEnumerable<Type> requiredWritableComponentTypes,
        IEnumerable<Type> requiredReadOnlyComponentTypes,
        bool requireInstantArchetypeCommand)
    {
        _matchedArchetypes = matchedArchetypes.ToArray();
        _requiredWritableComponentTypes = requiredWritableComponentTypes.ToArray();
        _requiredReadOnlyComponentTypes = requiredReadOnlyComponentTypes.ToArray();
        RequireInstantArchetypeCommand = requireInstantArchetypeCommand;

        _writableComponentStorages = CollectStorages(_requiredWritableComponentTypes);
        _readOnlyComponentStorages = CollectStorages(_requiredReadOnlyComponentTypes);
        _storages = CollectStorages(_requiredWritableComponentTypes.Concat(_requiredReadOnlyComponentTypes));
    }

    private FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>> CollectStorages(
        IEnumerable<Type> componentTypes)
    {
        return componentTypes.ToFrozenDictionary(
            componentType => componentType,
            componentType =>
            {
                var storages =
                    _matchedArchetypes.Select(archetype => archetype.ComponentStorageDictionary[componentType]);
                return ArrayFactory.StorageArrayFactories[componentType](storages);
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
}