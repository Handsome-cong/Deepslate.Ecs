using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class RecordCreateSystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [RequireInstantCommand]
    private Query _query;

    private readonly CommandBuffer _commandBuffer;
    private readonly int _count;

    public RecordCreateSystem(TickSystemBuilder builder, int count)
    {
        InitializeQuery(builder);
        builder.Build(this, out _);
        _commandBuffer = new CommandBuffer();
        _count = count;
    }

    public void Execute(Command command)
    {
        foreach (var _ in Enumerable.Range(0, _count))
        {
            command.RecordCreate(_commandBuffer, _query.MatchedArchetypes[0]);
        }

        command.ExecuteCommandBuffer(_commandBuffer);
    }
}