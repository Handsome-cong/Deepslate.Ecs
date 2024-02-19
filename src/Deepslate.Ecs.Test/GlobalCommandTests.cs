using Deepslate.Ecs.Extensions;

namespace Deepslate.Ecs.Test;

public sealed class GlobalCommandTests
{
    [DisabledFact]
    public void CreateEntity()
    {
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalArchetypeCommand();
        var entity = command.Create(positionArchetype);
        Assert.True(command.Contains(entity));
    }
    
    [DisabledFact]
    public void DestroyEntity()
    {
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalArchetypeCommand();
        var entity = command.Create(positionArchetype);
        Assert.True(command.Contains(entity));
        Assert.True(command.Destroy(entity));
        Assert.False(command.Contains(entity));
    }
    
    [DisabledFact]
    public void ModifyComponent()
    {
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalArchetypeCommand();
        var entity = command.Create(positionArchetype);
        ref var position = ref command.GetComponent<Position>(entity);
        position = new Position { X = 0, Y = 0, Z = 0 };
        ref var position2 = ref command.GetComponent<Position>(entity);
        Assert.Equal(position, position2);
    }

    [DisabledFact]
    public void ModifyComponents()
    {
        const int count = 2;
        using var world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out var positionArchetype)
            .Build();
        var command = world.CreateGlobalArchetypeCommand();
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
}