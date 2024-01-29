using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed partial class Archetype : IDisposable
{
    // TODO: Maybe an static empty archetype is useful.
    
    public readonly int Id;
    
    internal Archetype(int id, IEnumerable<IComponentStorage> storages)
    {
        Id = id;
        _componentStorages = storages.ToArray();
        _componentTypes = _componentStorages.Select(x => x.ComponentType).ToArray();
        ComponentStorageDictionary = _componentStorages
            .Select(x => new KeyValuePair<Type, IComponentStorage>(x.ComponentType, x))
            .ToFrozenDictionary();
    }
}

public sealed class ArchetypeInfo(IEnumerable<Type> componentTypes)
{
    private readonly SortedSet<Type> _componentTypes = new(componentTypes);
    public IReadOnlySet<Type> ComponentTypes => _componentTypes;

    public override int GetHashCode() => _componentTypes.Aggregate(0, HashCode.Combine);
}
