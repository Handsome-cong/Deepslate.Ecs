using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private readonly Type[] _componentTypes;
    private readonly IComponentStorage[] _componentStorages;
    
    internal FrozenDictionary<Type, IComponentStorage> ComponentStorageDictionary { get; }

    public IEnumerable<Type> ComponentTypes => _componentTypes;

    internal ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent: IComponent
    {
        var index = _entities.IndexOf(entity);
        if (index == EntityStorage.NoIndex)
        {
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity does not exist in this archetype.");
        }

        var componentSpan = GetStorage<TComponent>().AsSpan();
        return ref componentSpan[index];
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
        foreach (var storage in ComponentStorageDictionary.Values)
        {
            if (storage is IDisposable disposableStorage)
            {
                disposableStorage.Dispose();
            }
        }
    }
}
