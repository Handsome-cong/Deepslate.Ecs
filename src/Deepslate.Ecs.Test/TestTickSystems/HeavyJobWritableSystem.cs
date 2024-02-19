﻿using System.Diagnostics;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class HeavyJobWritableSystem<TComponent> : ITickSystemExecutor, ITimeRecorded
    where TComponent : IComponentData
{
    public long ElapsedTime { get; private set; }

    public HeavyJobWritableSystem(TickSystemBuilder builder)
    {
        builder.AddQuery()
            .AsGeneric()
            .WithWritable<TComponent>()
            .Build(out _);
    }
    
    public void Execute(EntityCommand command)
    {
        var sw = new Stopwatch();
        sw.Start();
        Thread.Sleep(TimeSpan.FromMilliseconds(HeavyJobSystem.ExecutionElapsedTime));
        sw.Stop();
        ElapsedTime = sw.ElapsedMilliseconds;
    }
}
public sealed class HeavyJobWritableSystem<TComponent1, TComponent2> : ITickSystemExecutor, ITimeRecorded
    where TComponent1 : IComponentData
    where TComponent2 : IComponentData
{
    public long ElapsedTime { get; private set; }

    public HeavyJobWritableSystem(TickSystemBuilder builder)
    {
        builder.AddQuery()
            .AsGeneric()
            .WithWritable<TComponent1>()
            .WithWritable<TComponent2>()
            .Build(out _);
    }
    
    public void Execute(EntityCommand command)
    {
        var sw = new Stopwatch();
        sw.Start();
        Thread.Sleep(TimeSpan.FromMilliseconds(HeavyJobSystem.ExecutionElapsedTime));
        sw.Stop();
        ElapsedTime = sw.ElapsedMilliseconds;
    }
}