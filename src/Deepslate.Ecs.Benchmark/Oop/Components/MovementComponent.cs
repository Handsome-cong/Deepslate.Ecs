namespace Deepslate.Ecs.Benchmark.Oop.Components;

public sealed class MovementComponent : Component
{
    private Position _position = default!;

    protected internal override void Startup()
    {
        if (!Entity.TryGetComponent(out Position pos))
        {
            pos = Entity.AddComponent<Position>();
        }

        _position = pos!;
    }

    protected internal override void Update()
    {
        _position.X++;
        _position.Y++;
        _position.Z++;
    }
}