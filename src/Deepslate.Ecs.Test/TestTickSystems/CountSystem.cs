namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class CountSystem : ITickSystemExecutor
{
    public int ExecutionCount { get; private set; } = 0;

    public CountSystem(TickSystemBuilder builder)
    {
        builder.AddQuery()
            .AsGeneric()
            .RequireReadOnly<Position>()
            .Build(out _);
    }
    public void Execute(TickSystemCommand command)
    {
        ExecutionCount++;
    }
}