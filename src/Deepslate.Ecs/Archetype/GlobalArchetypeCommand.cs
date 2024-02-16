namespace Deepslate.Ecs;

public readonly struct GlobalArchetypeCommand
{
    private readonly World _world;

    internal GlobalArchetypeCommand(World world)
    {
        _world = world;
    }

    public Entity Create(Archetype archetype)
    {
        return archetype.Create();
    }

    public bool Destroy(Entity entity)
    {
        return GetArchetype(entity)?.Destroy(entity) ?? false;
    }

    public ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent : IComponentData
    {
        var archetype = GetArchetype(entity);
        if (archetype == null)
        {
            throw new ArgumentOutOfRangeException(nameof(entity), "The entity does not belong to this world.");
        }
        return ref archetype.GetComponent<TComponent>(entity);
    }

    public Span<TComponent> GetComponents<TComponent>(Archetype archetype)
        where TComponent : IComponentData
    {
        return archetype.GetStorage<TComponent>().AsSpan();
    }
    
    public Archetype? GetArchetype(Entity entity)
    {
        var archetypeId = entity.ArchetypeId;
        return archetypeId < _world.Archetypes.Count ? _world.Archetypes[archetypeId] : null;
    }

    public bool Contains(Entity entity)
    {
        return GetArchetype(entity)?.ContainsEntity(entity) ?? false;
    }
}