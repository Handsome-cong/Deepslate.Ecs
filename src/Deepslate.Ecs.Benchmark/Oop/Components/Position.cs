namespace Deepslate.Ecs.Benchmark.Oop.Components;

public sealed class Position : Component
{
    public int X, Y, Z;
    protected internal override void Update()
    {
        X += 1;
        Y += 1;
        Z += 1;
    }
}