using System.Collections.Concurrent;

namespace Deepslate.Ecs;

/// <summary>
/// A reusable buffer that stores commands which can be executed later manually.
/// The add operations are thread-safe, but the execute operations are not.
/// </summary>
public sealed class CommandBuffer
{
    private readonly ConcurrentDictionary<Archetype, ConcurrentBag<DestructionCommandRecord>> _destructionCommands = [];

    private readonly ConcurrentDictionary<Archetype, ConcurrentBag<CreationCommandRecord>> _creationCommands = [];

    private int _creationCount;
    private int _destructionCount;

    internal void AddCreationCommand(Archetype archetype, CreationCommandRecord record)
    {
        _creationCommands.GetOrAdd(archetype, _ => [])
            .Add(record);
        _creationCount += record.Count;
    }

    internal void AddDestructionCommand(Archetype archetype, DestructionCommandRecord record)
    {
        _destructionCommands.GetOrAdd(archetype, _ => [])
            .Add(record);
        _destructionCount += record.Entities.Count();
    }

    /// <summary>
    /// Clear all commands in the buffer.
    /// </summary>
    public void Clear()
    {
        foreach (var (_, records) in _destructionCommands)
        {
            records.Clear();
        }
        
        foreach (var (_, records) in _creationCommands)
        {
            records.Clear();
        }

        _creationCount = 0;
        _destructionCount = 0;
    }

    internal void Execute()
    {
        foreach (var (archetype, destructionCommands) in _destructionCommands)
        {
            ExecuteDestructionCommandsWithSameArchetype(archetype, destructionCommands);
        }
        _destructionCount = 0;
        foreach (var (archetype, creationCommands) in _creationCommands)
        {
            ExecuteCreationCommandsWithSameArchetype(archetype, creationCommands);
        }
        _creationCount = 0;
    }

    internal async Task ParallelExecuteAsync()
    {
        await Parallel.ForEachAsync(_destructionCommands, (kvp, _) =>
        {
            ExecuteDestructionCommandsWithSameArchetype(kvp.Key, kvp.Value);
            return ValueTask.CompletedTask;
        });
        _destructionCount = 0;
        await Parallel.ForEachAsync(_creationCommands, (kvp, _) =>
        {
            ExecuteCreationCommandsWithSameArchetype(kvp.Key, kvp.Value);
            return ValueTask.CompletedTask;
        });
        _creationCount = 0;
    }

    private void ExecuteDestructionCommandsWithSameArchetype(
        Archetype archetype,
        ConcurrentBag<DestructionCommandRecord> destructionCommands)
    {
        var allEntities = new Entity[_destructionCount];
        var i = 0;
        foreach (var (entities, finalizer) in destructionCommands)
        {
            foreach (var entity in entities)
            {
                allEntities[i++] = entity;
                finalizer?.Invoke(new EntityComponentAccessor(entity, archetype));
            }
        }

        archetype.DestroyMany(allEntities);
        destructionCommands.Clear();
    }

    private void ExecuteCreationCommandsWithSameArchetype(
        Archetype archetype,
        ConcurrentBag<CreationCommandRecord> creationCommands)
    {
        var entities = archetype.CreateMany(_creationCount);
        var i = 0;
        foreach (var creationCommandRecord in creationCommands)
        {
            for (var j = 0; j < creationCommandRecord.Count; j++)
            {
                creationCommandRecord.Initializer?.Invoke(new EntityComponentAccessor(entities[i++], archetype));
            }
        }

        creationCommands.Clear();
    }
}