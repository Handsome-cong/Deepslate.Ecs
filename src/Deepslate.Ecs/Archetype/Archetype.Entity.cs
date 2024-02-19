using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private EntityStorage _entities = new();

    internal int Count => _entities.Count;
    internal ReadOnlySpan<Entity> Entities => _entities.AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool ContainsEntity(Entity entity) => _entities.IndexOf(entity) != EntityStorage.NoIndex;

    internal bool Destroy(Entity entity)
    {
        if (!_entities.RemoveEntity(entity, out var componentIndex))
        {
            return false;
        }

        foreach (var storage in _componentStorages)
        {
            storage.Remove(componentIndex);
        }

        return true;
    }

    internal void DestroyMany(Entity[] entities)
    {
        switch (entities)
        {
            case []:
                return;
            case [var entity]:
                Destroy(entity);
                return;
        }

        var removedIndices = _entities.RemoveEntities(entities);
        var sortedIndices = new List<int>(removedIndices.Length);
        for (var i = 0; i < removedIndices.Length; i++)
        {
            var index = removedIndices[i];
            if (index == EntityStorage.NoIndex)
            {
                continue;
            }
            
            sortedIndices.Add(index);
        }

        sortedIndices.Sort();
        foreach (var storage in _componentStorages)
        {
            storage.RemoveMany(CollectionsMarshal.AsSpan(sortedIndices));
        }
    }

    internal Entity Create()
    {
        var entity = _entities.AddEntity(Id);

        foreach (var storage in _componentStorages)
        {
            storage.Add();
        }

        return entity;
    }

    public Entity[] CreateMany(int count)
    {
        switch (count)
        {
            case <= 0:
                return Array.Empty<Entity>();
            case 1:
                return [Create()];
        }

        var entities = _entities.AddEntities(Id, count);

        // Different from destruction, creation is much more faster, especially when the empty space is enough.

        foreach (var storage in _componentStorages)
        {
            storage.Add(count);
        }

        return entities;
    }
}