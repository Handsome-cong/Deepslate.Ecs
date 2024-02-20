using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.TestTickSystems;

namespace Deepslate.Ecs.Test;

public sealed class WorldCreationTests
{
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
        builder.WithArchetypeAndBuild<Position>(out var positionArchetype);
        var stageBuilder = builder.AddStage();
        var systemBuilder = stageBuilder.AddTickSystem();
        systemBuilder.Build(new MovementSystem(systemBuilder));
        stageBuilder.Build();
        
        using var world = builder.Build();
        var command = world.CreateGlobalCommand();
        var entity = command.Create(positionArchetype);
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        world.Tick();
        Assert.True(Math.Abs(position.X - 1f) < 0.001f);
    }
}