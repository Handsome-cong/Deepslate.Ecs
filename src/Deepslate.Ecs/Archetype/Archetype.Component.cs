using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private readonly Type[] _componentTypes;
    private readonly IComponentStorage[] _componentStorages;

    internal FrozenDictionary<Type, IComponentStorage> ComponentStorageDictionary { get; }

    public IReadOnlyList<Type> ComponentTypes => _componentTypes;
    
    public bool ContainsComponent<TComponent>()
        where TComponent : IComponent => ComponentStorageDictionary.ContainsKey(typeof(TComponent));
    
    public int ComponentTypesHashCode { get; }

    internal ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent : IComponent
    {
        var index = _entities.IndexOf(entity);
        if (index == EntityStorage.NoIndex)
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Entity does not exist in this archetype.");
        }

        return ref GetComponents<TComponent>(index..(index + 1))[0];
    }

    internal bool TryGetComponent<TComponent>(Entity entity, out Span<TComponent> component)
        where TComponent : IComponent
    {
        var index = _entities.IndexOf(entity);
        if (index == EntityStorage.NoIndex)
        {
            component = Span<TComponent>.Empty;
            return false;
        }

        component = GetComponents<TComponent>(index..(index + 1));
        return true;
    }

    internal Span<TComponent> GetComponents<TComponent>(Range range)
        where TComponent : IComponent
    {
        if (!ComponentStorageDictionary.TryGetValue(typeof(TComponent), out var storage))
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Component does not exist in this archetype.");
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