namespace Deepslate.Ecs.Benchmark.Oop.Components;

public sealed class MovementComponent : Component
{
    private Position _position = default!;

    protected internal override void Startup()
    {
        if (!Entity.TryGetComponent(out _position!))
        {
            _position = Entity.AddComponent<Position>();
        }
    }

    protected internal override void Update()
    {
        _position.Move();
    }
}