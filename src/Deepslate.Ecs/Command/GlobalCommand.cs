namespace Deepslate.Ecs;

public readonly struct GlobalCommand
{
    private readonly World _world;

    internal GlobalCommand(World world)
    {
        _world = world;
    }

    public Entity Create(Archetype archetype)
    {
        return archetype.Create();
    }

    public Entity[] CreateMany(Archetype archetype, int count)
    {
        return archetype.CreateMany(count);
    }

    public bool Destroy(Entity entity)
    {
        return GetArchetype(entity)?.Destroy(entity) ?? false;
    }

    public ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent : IComponent
    {
        var archetype = GetArchetype(entity);
        if (archetype == null)
        {
            throw new ArgumentOutOfRangeException(nameof(entity), "The entity does not belong to this world.");
        }
        return ref archetype.GetComponent<TComponent>(entity);
    }

    public Span<TComponent> GetComponents<TComponent>(Archetype archetype)
        where TComponent : IComponent
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
    
    public TResource GetResource<TResource>()
        where TResource : IResource
    {
        if (!_world.ResourceFactories.TryGetValue(typeof(TResource), out var factory))
        {
            throw new InvalidOperationException("The resource has not been registered.");
        }

        if (factory is not Func<TResource> resourceFactory)
        {
            throw new InvalidOperationException("The resource factory is invalid.");
        }

        return resourceFactory();
    }
}