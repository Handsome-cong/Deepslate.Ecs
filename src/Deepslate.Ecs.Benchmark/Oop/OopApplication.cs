using Deepslate.Ecs.Benchmark.Oop.Components;

namespace Deepslate.Ecs.Benchmark.Oop;

public sealed class OopApplication
{
    private GameEntityManager _manager = new();
    
    public int EntityCount { get; set; } = 0;

    public void Prepare()
    {
        foreach (var i in Enumerable.Range(0, EntityCount))
        {
            var entity = _manager.CreateEntity();
            entity.AddComponent<Position>();
            entity.AddComponent<MovementComponent>();
        }
    }
    
    public void Start()
    {
        _manager.Update();
    }
}