namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class CountSystem : ITickSystemExecutor
{
    public int ExecutionCount { get; private set; }

    public CountSystem(TickSystemBuilder builder)
    {
        builder.AddQuery()
            .AsGeneric()
            .WithReadOnly<Position>()
            .Build(out _);
    }
    public void Execute(EntityCommand command)
    {
        ExecutionCount++;
    }
}