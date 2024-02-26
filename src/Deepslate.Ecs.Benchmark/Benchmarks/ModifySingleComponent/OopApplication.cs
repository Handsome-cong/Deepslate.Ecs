using Deepslate.Ecs.Benchmark.Oop;
using Deepslate.Ecs.Benchmark.Oop.Components;

namespace Deepslate.Ecs.Benchmark.Benchmarks.ModifySingleComponent;

public sealed class OopApplication
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
    }
    
    public void Start()
    {
        _manager.Update();
    }
}