using Deepslate.Ecs.GenericWrapper;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class MovementSystem : ITickSystemExecutor
{
    private Writable<Position>.ReadOnly.Query _positionQuery;

    public MovementSystem(TickSystemBuilder builder)
    {
        builder.AddQuery()
            .AsGeneric()
            .WithWritable<Position>()
            .Build(out _positionQuery);
    }
    
    public void Execute(EntityCommand command)
    {
        foreach (var bundle in _positionQuery)
        {
            ref var position = ref bundle.WritableComponent1;
            position.X++;
        }
    }
}