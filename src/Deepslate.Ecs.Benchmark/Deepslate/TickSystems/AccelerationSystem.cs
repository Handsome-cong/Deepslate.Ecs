using Deepslate.Ecs.Benchmark.Deepslate.Components;
using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Benchmark.Deepslate.TickSystems;

public sealed partial class AccelerationSystem : ITickSystemExecutor
{
    [WithWritable<Velocity>]
    [AsGenericQuery]
    private Query _query;

    public AccelerationSystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
    }
    
    public void Execute(Command command)
    {
        foreach (var storageSpan in _queryGeneric.Storages)
        {
            var velocities = storageSpan.Writable1Span;
            foreach (ref var velocity in velocities)
            {
                velocity.X++;
                velocity.Y++;
                velocity.Z++;
            }
        }
    }
}