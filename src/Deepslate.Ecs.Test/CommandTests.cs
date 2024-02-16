using Deepslate.Ecs.Extensions;

namespace Deepslate.Ecs.Test;

public sealed class CommandTests
{
    [Fact]
    public void CreateEntityByWorld()
    {
        var builder = new WorldBuilder();
        builder.WithArchetypeAndBuild<Position>(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateGlobalArchetypeCommand();
        var entity = command.Create(positionArchetype);
        Assert.True(command.Contains(entity));
    }
    
    [Fact]
    public void DestroyEntityByWorld()
    {
        var builder = new WorldBuilder();
        builder.WithArchetypeAndBuild<Position>(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateGlobalArchetypeCommand();
        var entity = command.Create(positionArchetype);
        Assert.True(command.Contains(entity));
        Assert.True(command.Destroy(entity));
        Assert.False(command.Contains(entity));
    }
    
    [Fact]
    public void GetComponentByWorld()
    {
        var builder = new WorldBuilder();
        builder.WithArchetypeAndBuild<Position>(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateGlobalArchetypeCommand();
        var entity = command.Create(positionArchetype);
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        ref var position2 = ref command.GetComponent<Position>(entity);
        Assert.Equal(position, position2);
    }
    
    [Fact]
    public void GetComponentByWorldWithMultipleComponents()
    {
        var builder = new WorldBuilder();
        builder.WithArchetypeAndBuild<Position>(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateGlobalArchetypeCommand();
        var entity = command.Create(positionArchetype);
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        ref var position2 = ref command.GetComponent<Position>(entity);
        Assert.Equal(position, position2);
        Assert.Throws<ArgumentOutOfRangeException>(() => command.GetComponent<Velocity>(entity));
    }
}