using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.TestTickSystems;

namespace Deepslate.Ecs.Test;

public sealed class CommandTests
{
    private const int CreationCount = 2;

    [Fact]
    public void Create()
    {
        TickSystem statisticsSystem = default!;
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>()
            .AddStage(stageBuilder =>
            {
                TickSystem createSystem = default!;
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new CreateSystem(tickSystemBuilder, CreationCount), out createSystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(createSystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem))
                    .Build();
            }).Build();

        var statisticsSystemExecutor = (StatisticsSystem)statisticsSystem.Executor;
        world.Tick();
        Assert.Equal(CreationCount, statisticsSystemExecutor.Count);
    }

    [Fact]
    public void Destroy()
    {
        TickSystem statisticsSystem1 = default!;
        TickSystem statisticsSystem2 = default!;
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>()
            .AddStage(stageBuilder =>
            {
                TickSystem createSystem = default!;
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new CreateSystem(tickSystemBuilder, CreationCount), out createSystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(createSystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem1))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(statisticsSystem1)
                            .Build(new DestroySystem(tickSystemBuilder)))
                    .Build();
            })
            .AddStage(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem2))
                    .Build();
            }).Build();

        var statisticsSystemExecutor1 = (StatisticsSystem)statisticsSystem1.Executor;
        var statisticsSystemExecutor2 = (StatisticsSystem)statisticsSystem2.Executor;
        world.Tick();
        Assert.Equal(CreationCount, statisticsSystemExecutor1.Count);
        Assert.Equal(0, statisticsSystemExecutor2.Count);
    }

    [Fact]
    public void RecordCreate()
    {
        TickSystem statisticsSystem = default!;
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>()
            .AddStage(stageBuilder =>
            {
                TickSystem createSystem = default!;
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new RecordCreateSystem(tickSystemBuilder, CreationCount),
                            out createSystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(createSystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem))
                    .Build();
            }).Build();

        var statisticsSystemExecutor = (StatisticsSystem)statisticsSystem.Executor;
        world.Tick();
        Assert.Equal(CreationCount, statisticsSystemExecutor.Count);
    }

    [Fact]
    public void RecordDestroy()
    {
        TickSystem statisticsSystem1 = default!;
        TickSystem statisticsSystem2 = default!;
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>()
            .AddStage(stageBuilder =>
            {
                TickSystem createSystem = default!;
                TickSystem destroySystem = default!;
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new RecordCreateSystem(tickSystemBuilder, CreationCount),
                            out createSystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(createSystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem1))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(statisticsSystem1)
                            .Build(new RecordDestroySystem(tickSystemBuilder), out destroySystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(destroySystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem2))
                    .Build();
            }).Build();

        var statisticsSystemExecutor1 = (StatisticsSystem)statisticsSystem1.Executor;
        var statisticsSystemExecutor2 = (StatisticsSystem)statisticsSystem2.Executor;
        world.Tick();
        Assert.Equal(CreationCount, statisticsSystemExecutor1.Count);
        Assert.Equal(0, statisticsSystemExecutor2.Count);
    }

    [Fact]
    public void DeferCreate()
    {
        TickSystem statisticsSystem1 = default!;
        TickSystem statisticsSystem2 = default!;
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>()
            .AddStage(stageBuilder =>
            {
                TickSystem createSystem = default!;
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new DeferCreateSystem(tickSystemBuilder, CreationCount),
                            out createSystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(createSystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem1))
                    .Build();
            })
            .AddStage(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem2))
                    .Build();
            }).Build();

        var statisticsSystemExecutor1 = (StatisticsSystem)statisticsSystem1.Executor;
        var statisticsSystemExecutor2 = (StatisticsSystem)statisticsSystem2.Executor;
        world.Tick();
        Assert.Equal(0, statisticsSystemExecutor1.Count);
        Assert.Equal(CreationCount, statisticsSystemExecutor2.Count);
    }

    [Fact]
    public void DeferDestroy()
    {
        TickSystem statisticsSystem1 = default!;
        TickSystem statisticsSystem2 = default!;
        TickSystem statisticsSystem3 = default!;
        TickSystem statisticsSystem4 = default!;

        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>()
            .AddStage(stageBuilder =>
            {
                TickSystem createSystem = default!;
                TickSystem destroySystem = default!;
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new DeferCreateSystem(tickSystemBuilder, CreationCount),
                            out createSystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(createSystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem1))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(statisticsSystem1)
                            .Build(new DeferDestroySystem(tickSystemBuilder), out destroySystem))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(destroySystem)
                            .Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem2))
                    .Build();
            })
            .AddStage(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem3))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder
                            .WithDependency(statisticsSystem1)
                            .Build(new DeferDestroySystem(tickSystemBuilder)))
                    .Build();
            })
            .AddStage(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new StatisticsSystem(tickSystemBuilder), out statisticsSystem4))
                    .Build();
            }).Build();

        var statisticsSystemExecutor1 = (StatisticsSystem)statisticsSystem1.Executor;
        var statisticsSystemExecutor2 = (StatisticsSystem)statisticsSystem2.Executor;
        var statisticsSystemExecutor3 = (StatisticsSystem)statisticsSystem3.Executor;
        var statisticsSystemExecutor4 = (StatisticsSystem)statisticsSystem4.Executor;
        world.Tick();
        Assert.Equal(0, statisticsSystemExecutor1.Count);
        Assert.Equal(0, statisticsSystemExecutor2.Count);
        Assert.Equal(CreationCount, statisticsSystemExecutor3.Count);
        Assert.Equal(0, statisticsSystemExecutor4.Count);
    }
}