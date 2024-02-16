using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class DestructionSystem : ITickSystemExecutor
{
    [With<Position>] [RequireInstantCommand] [AsGenericQuery]
    private Query _query;

    private readonly CommandBuffer _commandBuffer;

    public DestructionSystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
        builder.Build(this, out var tickSystem);
        _commandBuffer = new CommandBuffer(tickSystem);
    }

    public void Execute(TickSystemCommand systemCommand)
    {
        if (systemCommand.TryCreateInstantArchetypeCommand(_query, _query.MatchedArchetypes[0], out var command))
        {
            var instantCommand = command!.Value;
            List<Entity> entities = [];
            foreach (var entityComponentBundle in _queryGeneric)
            {
                entities.Add(entityComponentBundle.Entity);
            }

            instantCommand.RecordDestroy(_commandBuffer, entities);
            _commandBuffer.Execute();
        }
    }
}