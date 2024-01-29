using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private EntityStorage _entities = new();
    private readonly Queue<Entity> _removedEntities = new();
    
    internal ref EntityStorage Entities => ref _entities;
    
    internal int Count => _entities.Count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool Contains(Entity entity) => _entities.IndexOf(entity) != EntityStorage.NoIndex;

    internal bool Destroy(Entity entity)
    {
        var destroyed = _entities.RemoveEntity(entity, out var componentIndex);
        if (destroyed)
        {
            _removedEntities.Enqueue(entity);
            foreach (var storage in _componentStorages)
            {
                storage.Remove(componentIndex);
            }
        }
        
        return destroyed;
    }

    internal Entity Create()
    {
        if (_removedEntities.TryDequeue(out var entity))
        {
            entity = entity.BumpVersion();
        }
        else
        {
            entity = new Entity((uint)_entities.Count, (ushort)Id, 0);
        }

        // TODO: Result returned should be used somehow.
        var _ = _entities.AddEntity(entity);

        foreach (var storage in _componentStorages)
        {
            storage.Add();
        }

        return entity;
    }
}
