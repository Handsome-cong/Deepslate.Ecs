namespace Deepslate.Ecs.Benchmark.Oop.Components;

public sealed class ParallelAccelerationMovementComponent : Component
{
    private readonly List<Position> _positions = [];
    private readonly List<Velocity> _velocities = [];

    protected internal override void Startup()
    {
        foreach (var entity in Manager.Entities)
        {
            if (entity.TryGetComponent<Position>(out var position))
            {
                _positions.Add(position);
            }
            if (entity.TryGetComponent<Velocity>(out var velocity))
            {
                _velocities.Add(velocity);
            }
        }
    }

    protected internal override void Update()
    {
        var movement = Task.Run(() =>
        {
            foreach (var position in _positions)
            {
                position.Move();
            }
        });
        var acceleration = Task.Run(() =>
        {
            foreach (var velocity in _velocities)
            {
                velocity.Accelerate();
            }
        });
        
        Task.WaitAll(movement, acceleration);
    }
}