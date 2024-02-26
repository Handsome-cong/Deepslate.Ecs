using Deepslate.Ecs.Benchmark.Benchmarks.Common;
using Deepslate.Ecs.Benchmark.Oop;
using Deepslate.Ecs.Benchmark.Oop.Components;

namespace Deepslate.Ecs.Benchmark.Benchmarks.CreateEntity;

public sealed class OopApplication : IBenchmarkApplication
{
    private readonly GameEntityManager _manager = new();

    public int EntityCount { get; set; } = 0;

    public void Prepare()
    {
    }

    public void Start()
    {
        foreach (var i in Enumerable.Range(0, EntityCount))
        {
            var entity = _manager.CreateEntity();
            entity.AddComponent<Position>();
            entity.AddComponent<MovementComponent>();
        }
    }
}