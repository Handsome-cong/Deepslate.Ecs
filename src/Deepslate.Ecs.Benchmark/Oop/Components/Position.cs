﻿namespace Deepslate.Ecs.Benchmark.Oop.Components;

public sealed class Position : Component
{
    public int X, Y, Z;
    public void Move()
    {
        X++;
        Y++;
        Z++;
    }
}