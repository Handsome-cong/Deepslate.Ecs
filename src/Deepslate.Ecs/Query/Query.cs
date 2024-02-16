using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed class Query
{
    private readonly Archetype[] _matchedArchetypes;
    private readonly Type[] _requiredWritableComponentTypes;
    private readonly Type[] _requiredReadOnlyComponentTypes;

    private readonly FrozenDictionary<Type, IReadOnlyList<IComponentDataStorage>> _storages;

    public IReadOnlyList<Archetype> MatchedArchetypes => _matchedArchetypes;
    public FrozenSet<Archetype> MatchedArchetypesSet { get; }
    public IEnumerable<Type> RequiredWritableComponentTypes => _requiredWritableComponentTypes;
    public IEnumerable<Type> RequiredReadOnlyComponentTypes => _requiredReadOnlyComponentTypes;
    public bool RequireInstantArchetypeCommand { get; }


    internal Query(
        IEnumerable<Archetype> matchedArchetypes,
        IEnumerable<Type> requiredWritableComponentTypes,
        IEnumerable<Type> requiredReadOnlyComponentTypes,
        bool requireInstantArchetypeCommand,
        StorageArrayFactory storageArrayFactory)
    {
        _matchedArchetypes = matchedArchetypes.ToArray();
        _requiredWritableComponentTypes = requiredWritableComponentTypes.ToArray();
        _requiredReadOnlyComponentTypes = requiredReadOnlyComponentTypes.ToArray();
        RequireInstantArchetypeCommand = requireInstantArchetypeCommand;
        MatchedArchetypesSet = _matchedArchetypes.ToFrozenSet();

        _storages = CollectStorages(
            _requiredWritableComponentTypes.Concat(_requiredReadOnlyComponentTypes),
            storageArrayFactory);
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
}