using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class CreationSystem : ITickSystemExecutor
{
    [With<Position>] [RequireInstantCommand] [AsGenericQuery]
    private Query _query;

    private readonly CommandBuffer _commandBuffer;
    private readonly int _count;

    public CreationSystem(TickSystemBuilder builder, int count)
    {
        InitializeQuery(builder);
        builder.Build(this, out var tickSystem);
        _commandBuffer = new CommandBuffer(tickSystem);
        _count = count;
    }

    public void Execute(TickSystemCommand systemCommand)
    {
        if (systemCommand.TryCreateInstantArchetypeCommand(_query, _query.MatchedArchetypes[0], out var command))
        {
            var instantCommand = command!.Value;
            foreach (var i in Enumerable.Range(0, _count))
            {
                instantCommand.RecordCreate(_commandBuffer);
            }
            _commandBuffer.Execute();
        }
    }
}