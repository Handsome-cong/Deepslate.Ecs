using Deepslate.Ecs.GenericWrapper;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class MovementSystem : ITickSystemExecutor
{
    private readonly Writable<Position>.ReadOnly.Query _positionQuery;

    public MovementSystem(TickSystemBuilder builder)
    {
        builder.AddQuery()
            .AsGeneric()
            .RequireWritable<Position>()
            .Build(out _positionQuery);
    }
    
    public void Execute(TickSystemCommand command)
    {
        foreach (var bundle in _positionQuery)
        {
            ref var position = ref bundle.WritableComponent1;
            position.X++;
        }
    }
}