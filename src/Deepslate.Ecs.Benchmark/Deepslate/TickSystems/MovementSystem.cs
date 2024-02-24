using Deepslate.Ecs.Benchmark.Deepslate.Components;
using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Benchmark.Deepslate.TickSystems;

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