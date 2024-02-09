using Deepslate.Ecs.Test.TestTickSystems;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Deepslate.Ecs.Test;

public sealed class WorldCreationTests(ITestOutputHelper outputHelper)
{
    private ITestOutputHelper _outputHelper = outputHelper;
    
    [Fact]
    public void EmptyWorld()
    {
        var builder = new WorldBuilder();

        using var world = builder.Build();
        world.Tick();
    }
    
    [Fact]
    public void WorldWithComponents()
    {
        var builder = new WorldBuilder();
        builder.WithManagedComponent<Velocity>();
        builder.WithUnmanagedComponent<Position>();
        using var world = builder.Build();
        world.Tick();
    }
    
    [Fact]
    public void WorldWithSystems()
    {
        var builder = new WorldBuilder();
        builder.WithArchetype()
            .WithComponent<Position>()
            .Build(out var positionArchetype);
        var stageBuilder = builder.AddStage();
        var systemBuilder = stageBuilder.AddTickSystem();
        systemBuilder.Build(new MovementSystem(systemBuilder), out _);
        stageBuilder.Build(out _);
        
        using var world = builder.Build();
        var command = world.CreateInstantArchetypeCommand(positionArchetype);
        var entity = command.Create();
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        world.Tick();
        Assert.True(Math.Abs(position.X - 1f) < 0.001f);
    }
}