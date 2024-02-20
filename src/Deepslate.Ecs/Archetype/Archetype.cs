using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed partial class Archetype : IDisposable
{
    public static readonly Archetype Empty = new(ushort.MaxValue, Array.Empty<IComponentStorage>());

    internal readonly ushort Id;

    internal Archetype(ushort id, IEnumerable<IComponentStorage> storages)
    {
        Id = id;
        _componentStorages = storages.OrderBy(storage => storage.ComponentType.GetHashCode()).ToArray();
        
        _componentTypes = _componentStorages.Select(x => x.ComponentType).ToArray();
        ComponentStorageDictionary = _componentStorages
            .Select(x => new KeyValuePair<Type, IComponentStorage>(x.ComponentType, x))
            .ToFrozenDictionary();

        ComponentTypesHashCode = _componentTypes.Aggregate(0, HashCode.Combine);
    }
}