namespace Deepslate.Ecs.Benchmark.Oop.Components;

public sealed class Velocity : Component
{
    public int X, Y, Z;

    public void Accelerate()
    {
        X++;
        Y++;
        Z++;
    }
}