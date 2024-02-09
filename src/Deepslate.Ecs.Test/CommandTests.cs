namespace Deepslate.Ecs.Test;

public sealed class CommandTests
{
    [Fact]
    public void CreateEntityByWorld()
    {
        var builder = new WorldBuilder();
        builder.WithArchetype()
            .WithComponent<Position>()
            .Build(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateInstantArchetypeCommand(positionArchetype);
        var entity = command.Create();
        Assert.True(command.Contains(entity));
    }
    
    [Fact]
    public void DestroyEntityByWorld()
    {
        var builder = new WorldBuilder();
        builder.WithArchetype()
            .WithComponent<Position>()
            .Build(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateInstantArchetypeCommand(positionArchetype);
        var entity = command.Create();
        Assert.True(command.Contains(entity));
        Assert.True(command.Destroy(entity));
        Assert.False(command.Contains(entity));
    }
    
    [Fact]
    public void GetComponentByWorld()
    {
        var builder = new WorldBuilder();
        builder.WithArchetype()
            .WithComponent<Position>()
            .Build(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateInstantArchetypeCommand(positionArchetype);
        var entity = command.Create();
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        ref var position2 = ref command.GetComponent<Position>(entity);
        Assert.Equal(position, position2);
    }
    
    [Fact]
    public void GetComponentByWorldWithMultipleComponents()
    {
        var builder = new WorldBuilder();
        builder.WithArchetype()
            .WithComponent<Position>()
            .WithComponent<Velocity>()
            .Build(out var positionArchetype);
        using var world = builder.Build();
        var command = world.CreateInstantArchetypeCommand(positionArchetype);
        var entity = command.Create();
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        ref var position2 = ref command.GetComponent<Position>(entity);
        Assert.Equal(position, position2);
        ref var rotation = ref command.GetComponent<Velocity>(entity);
        rotation = new Velocity { X = 0, Y = 0, Z = 0 };
        ref var rotation2 = ref command.GetComponent<Velocity>(entity);
        Assert.Equal(rotation, rotation2);
    }
}