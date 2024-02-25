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
        foreach (var storageSpan in _queryGeneric.Storages)
        {
            var positions = storageSpan.Writable1Span;
            foreach (ref var position in positions)
            {
                position.X++;
                position.Y++;
                position.Z++;
            }
        }
    }
}