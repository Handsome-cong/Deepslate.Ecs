namespace Deepslate.Ecs.Benchmark.Oop;

public abstract class Component
{
    public GameEntity Entity { get; internal set; } = default!;
    
    protected internal virtual void Startup()
    {
    }
    
    protected internal virtual void Update()
    {
    }
}