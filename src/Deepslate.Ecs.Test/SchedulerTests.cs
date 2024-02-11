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
        systemBuilder.Build(new HeavyJobReadOnlySystem<Position>(systemBuilder), out var system1);
        systemBuilder = stageBuilder.AddTickSystem();
        systemBuilder.Build(new HeavyJobReadOnlySystem<Position>(systemBuilder), out var system2);
        stageBuilder.Build();
        using var world = builder.Build();
        var sw = new Stopwatch();
        sw.Start();
        world.Tick();
        sw.Stop();
        Assert.True(sw.ElapsedMilliseconds < HeavyJobSystem.ExecutionElapsedTime * 2);
        var elapsed1 = ((HeavyJobReadOnlySystem<Position>)system1.Executor).ElapsedTime;
        var elapsed2 = ((HeavyJobReadOnlySystem<Position>)system2.Executor).ElapsedTime;
        outputHelper.WriteLine($"System1 executed: {elapsed1}ms");
        outputHelper.WriteLine($"System2 executed: {elapsed2}ms");
        outputHelper.WriteLine($"Consumed time: {sw.ElapsedMilliseconds}ms");
    }

    [Fact]
    public void ParallelExecutionWithConflict()
    {
        List<TickSystem> systemsWithTime = [];
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position, Velocity>()
            .AddStage(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.Build(new HeavyJobReadOnlySystem<Position>(tickSystemBuilder),
                            out var tickSystem);
                        systemsWithTime.Add(tickSystem);
                    })
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.Build(new HeavyJobWritableSystem<Position>(tickSystemBuilder),
                            out var tickSystem);
                        systemsWithTime.Add(tickSystem);
                    })
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.Build(new HeavyJobWritableSystem<Velocity>(tickSystemBuilder),
                            out var tickSystem);
                        systemsWithTime.Add(tickSystem);
                    })
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.AddQuery()
                            .RequireReadOnly<Position>()
                            .RequireWritable<Velocity>()
                            .Build(out _)
                            .Build(new HeavyJobSystem(), out var tickSystem);
                        systemsWithTime.Add(tickSystem);
                    })
                    .Build();
            })
            .Build();
        var sw = new Stopwatch();
        sw.Start();
        world.Tick();
        sw.Stop();
        var systemsElapsedTime = systemsWithTime.Aggregate(0L,
            (current, system) => ((ITimeRecorded)system.Executor).ElapsedTime + current);
        Assert.True(systemsElapsedTime >= systemsWithTime.Count * HeavyJobSystem.ExecutionElapsedTime);
        Assert.True(sw.ElapsedMilliseconds < systemsElapsedTime);
        
        outputHelper.WriteLine($"Systems elapsed time: {systemsElapsedTime}");
        outputHelper.WriteLine($"Actually Consumed time: {sw.ElapsedMilliseconds}");
        Assert.True(Math.Abs(sw.ElapsedMilliseconds - 3 * HeavyJobSystem.ExecutionElapsedTime) < HeavyJobSystem.ExecutionElapsedTime);
    }
}