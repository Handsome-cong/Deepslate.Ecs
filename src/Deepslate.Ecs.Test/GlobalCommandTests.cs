using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.TestResources;

namespace Deepslate.Ecs.Test;

public sealed class GlobalCommandTests
{
    [Fact]
    public void CreateEntity()
    {
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalCommand();
        var entity = command.Create(positionArchetype);
        Assert.True(command.Contains(entity));
    }
    
    [Fact]
    public void DestroyEntity()
    {
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalCommand();
        var entity = command.Create(positionArchetype);
        Assert.True(command.Contains(entity));
        Assert.True(command.Destroy(entity));
        Assert.False(command.Contains(entity));
    }
    
    [Fact]
    public void ModifyComponent()
    {
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalCommand();
        var entity = command.Create(positionArchetype);
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        ref var position2 = ref command.GetComponent<Position>(entity);
        Assert.Equal(position, position2);
    }

    [Fact]
    public void ModifyComponents()
    {
        const int count = 2;
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalCommand();
        var entities = command.CreateMany(positionArchetype, count);
        var positions = command.GetComponents<Position>(positionArchetype);
        
        Assert.Equal(count, entities.Length);
        Assert.Equal(count, positions.Length);
        
        for (var i = 0; i < count; i++)
        {
            ref var position = ref positions[i];
            position = new Position { X = i, Y = i, Z = i };
        }
        
        var positions2 = command.GetComponents<Position>(positionArchetype);
        for (var i = 0; i < count; i++)
        {
            Assert.Equal(positions[i], positions2[i]);
        }
    }

    [Fact]
    public void GetResource()
    {
        using var world = new WorldBuilder()
            .WithResource(new CounterResourceFactory().Create)
            .Build();
        var command = world.CreateGlobalCommand();
        var resource1 = command.GetResource<CounterResource>();
        var resource2 = command.GetResource<CounterResource>();
        Assert.Equal(1, resource2.Value - resource1.Value);
    }
}