using Deepslate.Ecs.GenericWrapper;
using Xunit.Abstractions;

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
    
    public void Execute()
    {
        foreach (var bundle in _positionQuery)
        {
            ref var position = ref bundle.WritableComponent1;
            position.X++;
        }
    }
}