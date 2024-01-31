using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private readonly Type[] _componentTypes;
    private readonly IComponentStorage[] _componentStorages;

    internal FrozenDictionary<Type, IComponentStorage> ComponentStorageDictionary { get; }

    public IEnumerable<Type> ComponentTypes => _componentTypes;
    
    public bool ContainsComponent<TComponent>()
        where TComponent : IComponent => ComponentStorageDictionary.ContainsKey(typeof(TComponent));

    internal Span<TComponent> GetComponent<TComponent>(Entity entity)
        where TComponent : IComponent
    {
        var index = _entities.IndexOf(entity);
        if (index == EntityStorage.NoIndex)
        {
            return Span<TComponent>.Empty;
        }

        return GetComponents<TComponent>(index..(index + 1));
    }

    internal Span<TComponent> GetComponents<TComponent>(Range range)
        where TComponent : IComponent
    {
        if (!ComponentStorageDictionary.TryGetValue(typeof(TComponent), out var storage))
        {
            return Span<TComponent>.Empty;
        }

        var (offset, length) = range.GetOffsetAndLength(Count);
        return ((IComponentStorage<TComponent>)storage).AsSpan().Slice(offset, length);
    }

    internal IComponentStorage<TComponent> GetStorage<TComponent>()
        where TComponent : IComponent
    {
        if (!ComponentStorageDictionary.TryGetValue(typeof(TComponent), out var storage))
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Component does not exist in this archetype.");
        }

        return (IComponentStorage<TComponent>)storage;
    }

    public void Dispose()
    {
        foreach (var storage in _componentStorages)
        {
            storage.Dispose();
        }
    }
}