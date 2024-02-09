using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private uint _maxEntityId = 0;
    private EntityStorage _entities = new();
    private readonly Queue<Entity> _removedEntities = new();

    internal int Count => _entities.Count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool ContainsEntity(Entity entity) => _entities.IndexOf(entity) != EntityStorage.NoIndex;

    internal ReadOnlySpan<Entity> GetEntities(Range range)
    {
        var (offset, length) = range.GetOffsetAndLength(Count);
        return _entities.AsSpan().Slice(offset, length);
    }

    internal bool Destroy(Entity entity)
    {
        if (!_entities.RemoveEntity(entity, out var componentIndex))
        {
            return false;
        }

        _removedEntities.Enqueue(entity);
        foreach (var storage in _componentStorages)
        {
            storage.Remove(componentIndex);
        }

        return true;
    }

    private void DestroyMany(Entity[] entities)
    {
        switch (entities)
        {
            case []:
                return;
            case [var entity]:
                Destroy(entity);
                return;
        }

        var sortedIndices = new int[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var entity = entities[i];
            if (!_entities.RemoveEntity(entity, out var componentIndex))
            {
                continue;
            }

            _removedEntities.Enqueue(entity);
            sortedIndices[i] = componentIndex;
        }

        Array.Sort(sortedIndices);
        foreach (var storage in _componentStorages)
        {
            storage.RemoveMany(sortedIndices);
        }
    }

    internal Entity Create()
    {
        if (_removedEntities.TryDequeue(out var entity))
        {
            entity = entity.BumpVersion();
        }
        else
        {
            entity = new Entity(_maxEntityId++, Id, 0);
        }

        _ = _entities.AddEntity(entity);

        foreach (var storage in _componentStorages)
        {
            storage.Add();
        }

        return entity;
    }

    private Entity[] CreateMany(int count)
    {
        switch (count)
        {
            case <= 0:
                return Array.Empty<Entity>();
            case 1:
                return [Create()];
        }

        var entities = new Entity[count];

        var currentEntityCount = 0;
        for (; currentEntityCount < count; ++currentEntityCount)
        {
            if (!_removedEntities.TryDequeue(out var entity))
            {
                break;
            }

            entities[currentEntityCount] = entity.BumpVersion();
        }

        for (; currentEntityCount < count; ++currentEntityCount)
        {
            entities[currentEntityCount] = new Entity(_maxEntityId++, Id, 0);
        }

        foreach (var entity in entities)
        {
            _entities.AddEntity(entity);
        }

        // Different from destruction, creation is much more faster, especially when the empty space is enough.

        foreach (var storage in _componentStorages)
        {
            storage.Add(count);
        }

        return entities;
    }
}