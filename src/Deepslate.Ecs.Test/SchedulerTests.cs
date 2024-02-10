using System.Diagnostics;
using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.TestTickSystems;
using Xunit.Abstractions;

namespace Deepslate.Ecs.Test;

public sealed class SchedulerTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public void ParallelExecution()
    {
        var builder = new WorldBuilder();
        builder.WithArchetypeAndBuild<Position>(out var positionArchetype);
        var stageBuilder = builder.AddStage();
        var systemBuilder = stageBuilder.AddTickSystem();
        systemBuilder.Build(new HeavyJobSystem<Position>(systemBuilder), out var system1);
        systemBuilder = stageBuilder.AddTickSystem();
        systemBuilder.Build(new HeavyJobSystem<Position>(systemBuilder), out var system2);
        stageBuilder.Build();
        using var world = builder.Build();
        var sw = new Stopwatch();
        sw.Start();
        world.Tick();
        sw.Stop();
        Assert.True(sw.ElapsedMilliseconds < HeavyJobSystem<Position>.ExecutionElapsedTime * 2);
        var elapsed1 = ((HeavyJobSystem<Position>)system1.Executor).ElapsedTime;
        var elapsed2 = ((HeavyJobSystem<Position>)system2.Executor).ElapsedTime;
        outputHelper.WriteLine($"System1 executed: {elapsed1}ms");
        outputHelper.WriteLine($"System2 executed: {elapsed2}ms");
        outputHelper.WriteLine($"Consumed time: {sw.ElapsedMilliseconds}ms");
    }
}