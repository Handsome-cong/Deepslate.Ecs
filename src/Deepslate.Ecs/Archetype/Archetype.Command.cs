using System.Collections.Concurrent;

namespace Deepslate.Ecs;

public sealed partial class Archetype
{
    private readonly ConcurrentBag<CreationCommand> _creationCommandsEndOfStage = [];
    private readonly ConcurrentBag<CreationCommand> _creationCommandsEndOfTick = [];
    private readonly ConcurrentBag<DestructionCommand> _destructionCommandsEndOfStage = [];
    private readonly ConcurrentBag<DestructionCommand> _destructionCommandsEndOfTick = [];
    
    internal void AddCreationCommand(
        Action<EntityComponentAccessor>? initializer,
        DeferredArchetypeCommand.ExecuteTiming timing)
    {
        switch (timing)
        {
            case DeferredArchetypeCommand.ExecuteTiming.EndOfStage:
                _creationCommandsEndOfStage.Add(new CreationCommand(initializer));
                break;
            case DeferredArchetypeCommand.ExecuteTiming.EndOfTick:
                _creationCommandsEndOfTick.Add(new CreationCommand(initializer));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(timing), timing, "Invalid timing.");
        }
    }

    internal void AddDestructionCommand(
        Entity entity,
        Action<EntityComponentAccessor>? finalizer,
        DeferredArchetypeCommand.ExecuteTiming timing)
    {
        switch (timing)
        {
            case DeferredArchetypeCommand.ExecuteTiming.EndOfStage:
                _destructionCommandsEndOfStage.Add(new DestructionCommand(entity, finalizer));
                break;
            case DeferredArchetypeCommand.ExecuteTiming.EndOfTick:
                _destructionCommandsEndOfTick.Add(new DestructionCommand(entity, finalizer));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(timing), timing, "Invalid timing.");
        }
    }

    internal void ExecuteEndOfStageCommands()
    {
        ExecuteDestructionCommands(_destructionCommandsEndOfStage);
        ExecuteCreationCommands(_creationCommandsEndOfStage);
    }
    
    internal void ExecuteEndOfTickCommands()
    {
        ExecuteDestructionCommands(_destructionCommandsEndOfTick);
        ExecuteCreationCommands(_creationCommandsEndOfTick);
    }

    private void ExecuteDestructionCommands(ConcurrentBag<DestructionCommand> destructionCommands)
    {
        foreach (var (entity, finalizer) in destructionCommands)
        {
            finalizer?.Invoke(new EntityComponentAccessor(entity, this));
        }

        var entities = destructionCommands.Select(command => command.Entity).ToArray();
        DestroyMany(entities);
        destructionCommands.Clear();
    }

    private void ExecuteCreationCommands(ConcurrentBag<CreationCommand> creationCommands)
    {
        var count = creationCommands.Count;
        var entities = CreateMany(count);
        var i = 0;
        foreach (var creationCommand in creationCommands)
        {
            var entity = entities[i++];
            creationCommand.Initializer?.Invoke(new EntityComponentAccessor(entity, this));
        }
    }

    private readonly record struct CreationCommand(
        Action<EntityComponentAccessor>? Initializer);

    private readonly record struct DestructionCommand(
        Entity Entity,
        Action<EntityComponentAccessor>? Finalizer);
}