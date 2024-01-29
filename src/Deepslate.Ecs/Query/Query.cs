namespace Deepslate.Ecs;

public readonly struct Query
{
    public static readonly Query Empty = new();

    private readonly Archetype[] _matchedArchetypes;
    private readonly Type[] _requiredComponentTypes;

    private readonly IComponentStorage[][] _storages;

    public IReadOnlyList<Archetype> MatchedArchetypes => _matchedArchetypes;
    public IReadOnlyList<Type> RequiredComponentTypes => _requiredComponentTypes;

    internal Query(Archetype[] matchedArchetypes, Type[] requiredComponentTypes)
    {
        _matchedArchetypes = matchedArchetypes;
        _requiredComponentTypes = requiredComponentTypes;
        _storages = CollectStorages();
    }

    private IComponentStorage[][] CollectStorages()
    {
        var matchedArchetypes = _matchedArchetypes;
        return _requiredComponentTypes.Select(requiredComponentType =>
                matchedArchetypes.Select(archetype => archetype.ComponentStorageDictionary[requiredComponentType])
                    .ToArray())
            .ToArray();
    }

    internal IComponentStorage<TComponent>[] GetStorages<TComponent>()
        where TComponent : IComponent
    {
        var index = 0;
        for (; index < _requiredComponentTypes.Length; index++)
        {
            if (_requiredComponentTypes[index] == typeof(TComponent))
            {
                break;
            }
        }

        if (index >= _requiredComponentTypes.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Component type not found in query");
        }

        return (IComponentStorage<TComponent>[])_storages[index];
    }
}
