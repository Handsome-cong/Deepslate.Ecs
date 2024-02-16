using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.TestTickSystems;
using Xunit.Abstractions;

namespace Deepslate.Ecs.Test;

public sealed class ReactSystemTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public void AfterAlloc()
    {
        var allocated = false;
        using var world = new WorldBuilder()
            .WithReactAfterAlloc<Position>(_ => allocated = true)
            .WithArchetypeAndBuild<Position, Velocity>(out var archetype)
            .AddStage(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                {
                    tickSystemBuilder.Build(new CreationSystem(tickSystemBuilder, 9));
                }).Build();
            }).Build();

        world.Tick();
        Assert.True(allocated);
    }

    [Fact]
    public void AfterCreate()
    {
        using var world = new WorldBuilder()
            .WithReactAfterCreate<Position>(component =>
            {
                if (component.Length > 0)
                {
                    ref var pos = ref component[0];
                    pos = Position.One;
                }
            })
            .WithArchetypeAndBuild<Position, Velocity>(out var archetype)
            .AddStage(stageBuilder =>
            {
                stageBuilder
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.Build(new CreationSystem(tickSystemBuilder, 9));
                    }).Build();
            }).Build();

        world.Tick();
        var command = world.CreateGlobalArchetypeCommand();
        var pos = command.GetComponents<Position>(archetype)[0];
        Assert.Equal(Position.One, pos);
    }

    [Fact]
    public void BeforeDestroy()
    {
        const int creationCount = 10;
        var destroyedCount = 0;
        using var world = new WorldBuilder()
            .WithReactBeforeDestroy<Position>((ref Position pos) => destroyedCount++)
            .WithArchetypeAndBuild<Position, Velocity>(out var archetype)
            .AddStage(stageBuilder =>
            {
                stageBuilder
                    .AddTickSystem(
                        tickSystemBuilder =>
                        {
                            tickSystemBuilder.Build(new CreationSystem(tickSystemBuilder, creationCount));
                        }, out var creationSystem)
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.WithDependency(creationSystem!);
                        tickSystemBuilder.Build(new DestructionSystem(tickSystemBuilder));
                    }).Build();
            }).Build();

        world.Tick();
        Assert.Equal(creationCount, destroyedCount);
    }

    [Fact]
    public void BeforeFree()
    {
        const int creationCount = 10;
        var releasedCount = 0;
        var worldBuilder = new WorldBuilder()
            .WithReactBeforeFree<Position>(positions => releasedCount += positions.Length)
            .WithArchetypeAndBuild<Position, Velocity>(out var archetype)
            .AddStage(stageBuilder =>
            {
                stageBuilder
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.Build(new CreationSystem(tickSystemBuilder, creationCount));
                    }).Build();
            });
        using (var world = worldBuilder.Build())
        {
            world.Tick();
        }

        Assert.Equal(creationCount, releasedCount);
    }

    [Fact]
    public void BeforeMove()
    {
        const int firstCreationCount = 5;
        const int secondCreationCount = 1000;
        var movedCount = 0;
        using var world = new WorldBuilder()
            .WithReactBeforeMove<Position>((from, to) => movedCount += from.Length)
            .WithArchetypeAndBuild<Position, Velocity>(out var archetype)
            .AddStage(stageBuilder =>
            {
                stageBuilder
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.Build(new CreationSystem(tickSystemBuilder, firstCreationCount));
                    })
                    .AddTickSystem(tickSystemBuilder =>
                    {
                        tickSystemBuilder.Build(new CreationSystem(tickSystemBuilder, secondCreationCount));
                    }).Build();
            }).Build();

        world.Tick();
        Assert.Equal(firstCreationCount, movedCount);
    }
}