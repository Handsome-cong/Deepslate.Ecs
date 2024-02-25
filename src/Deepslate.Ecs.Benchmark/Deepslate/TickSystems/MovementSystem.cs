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