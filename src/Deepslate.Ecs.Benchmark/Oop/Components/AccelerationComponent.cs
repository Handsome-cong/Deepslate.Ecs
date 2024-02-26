namespace Deepslate.Ecs.Benchmark.Oop.Components;

public class AccelerationComponent : Component
{
    private Velocity _velocity = default!;
    
    protected internal override void Startup()
    {
        if (!Entity.TryGetComponent(out _velocity!))
        {
            _velocity = Entity.AddComponent<Velocity>();
        }
    }

    protected internal override void Update()
    {
        _velocity.Accelerate();
    }
}