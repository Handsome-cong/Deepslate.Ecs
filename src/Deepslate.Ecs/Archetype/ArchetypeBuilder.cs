namespace Deepslate.Ecs;

public sealed class ArchetypeBuilder
{
    private readonly HashSet<Type> _componentTypes = [];
    public WorldBuilder WorldBuilder { get; }
    public IReadOnlySet<Type> ComponentTypes => _componentTypes;
    public Archetype? Result { get; private set; }

    internal ArchetypeBuilder(WorldBuilder worldWorldBuilder)
    {
        WorldBuilder = worldWorldBuilder;
    }

    public ArchetypeBuilder WithComponent<TComponent>()
        where TComponent : IComponent
    {
        _componentTypes.Add(typeof(TComponent));
        return this;
    }

    public WorldBuilder Build(out Archetype configuredArchetype, out bool newArchetypeRegistered)
    {
        if (Result is not null)
        {
            configuredArchetype = Result;
            newArchetypeRegistered = false;
            return WorldBuilder;
        }

        var storages = new IComponentStorage[_componentTypes.Count];
        var count = 0;
        foreach (var componentType in _componentTypes)
        {
            if (!WorldBuilder.ComponentTypes.Contains(componentType))
            {
                WorldBuilder.WithUnmanagedComponentIfPossible(componentType);
            }

            storages[count++] = WorldBuilder.ComponentStorageFactory.Factories[componentType](WorldBuilder);
        }

        var id = WorldBuilder.NextArchetypeId;
        configuredArchetype = new Archetype(id, storages);
        newArchetypeRegistered = WorldBuilder.TryRegisterArchetype(configuredArchetype, out configuredArchetype);
        Result = configuredArchetype;
        return WorldBuilder;
    }
}