using Deepslate.Ecs.Benchmark.Ecs.Components;
using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Benchmark.Ecs.TickSystems;

public sealed partial class MovementSystem : ITickSystemExecutor
{
    [WithWritable<Position>]
    [AsGenericQuery]
    private Query _query;
    
    public MovementSystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
    }
    
    public void Execute(Command command)
    {
        foreach (ref var queryResult in _queryGeneric)
        {
            ref var position = ref queryResult.WritableComponent1;
            position.X += 1;
            position.Y += 1;
            position.Z += 1;
        }
    }
}