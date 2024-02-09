using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private readonly Type[] _componentTypes;
    private readonly IComponentDataStorage[] _componentStorages;

    internal FrozenDictionary<Type, IComponentDataStorage> ComponentStorageDictionary { get; }

    public IReadOnlyList<Type> ComponentTypes => _componentTypes;
    
    public bool ContainsComponent<TComponent>()
        where TComponent : IComponentData => ComponentStorageDictionary.ContainsKey(typeof(TComponent));
    
    public int ComponentTypesHashCode { get; }

    internal ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent : IComponentData
    {
        var index = _entities.IndexOf(entity);
        if (index == EntityStorage.NoIndex)
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Component does not exist in this archetype.");
        }

        return ref GetComponents<TComponent>(index..(index + 1))[0];
    }

    internal bool TryGetComponent<TComponent>(Entity entity, out Span<TComponent> component)
        where TComponent : IComponentData
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
        where TComponent : IComponentData
    {
        if (!ComponentStorageDictionary.TryGetValue(typeof(TComponent), out var storage))
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Component does not exist in this archetype.");
        }

        var (offset, length) = range.GetOffsetAndLength(Count);
        return ((IComponentDataStorage<TComponent>)storage).AsSpan().Slice(offset, length);
    }

    internal IComponentDataStorage<TComponent> GetStorage<TComponent>()
        where TComponent : IComponentData
    {
        if (!ComponentStorageDictionary.TryGetValue(typeof(TComponent), out var storage))
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Component does not exist in this archetype.");
        }

        return (IComponentDataStorage<TComponent>)storage;
    }

    public void Dispose()
    {
        foreach (var storage in _componentStorages)
        {
            storage.Dispose();
        }
    }
}