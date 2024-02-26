using Deepslate.Ecs.Benchmark.Benchmarks.Common;
using Deepslate.Ecs.Benchmark.Oop;
using Deepslate.Ecs.Benchmark.Oop.Components;

namespace Deepslate.Ecs.Benchmark.Benchmarks.ModifyDualComponentsInParallel;

public sealed class OopApplication : IBenchmarkApplication
{
    private readonly GameEntityManager _manager = new();

    public int EntityCount { get; set; }

    public void Prepare()
    {
        foreach (var i in Enumerable.Range(0, EntityCount))
        {
            var entity = _manager.CreateEntity();
            entity.AddComponent<Position>();
            entity.AddComponent<Velocity>();
        }

        var e = _manager.CreateEntity();
        e.AddComponent<ParallelAccelerationMovementComponent>();
    }

    public void Start()
    {
        _manager.Update();
    }
}