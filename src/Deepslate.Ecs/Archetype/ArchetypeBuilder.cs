namespace Deepslate.Ecs;

public sealed class ArchetypeBuilder
{
    private readonly WorldBuilder _worldBuilder;
    private readonly HashSet<Type> _componentTypes = [];
    public IEnumerable<Type> ComponentTypes => _componentTypes;
    public Archetype? Result { get; private set; }

    internal ArchetypeBuilder(WorldBuilder worldBuilder)
    {
        _worldBuilder = worldBuilder;
    }

    public ArchetypeBuilder WithComponent<TComponent>()
        where TComponent : IComponentData
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
            return _worldBuilder;
        }

        var storages = new IComponentDataStorage[_componentTypes.Count];
        var count = 0;
        foreach (var componentType in _componentTypes)
        {
            if (!_worldBuilder.ComponentTypes.Contains(componentType))
            {
                _worldBuilder.WithUnmanagedComponentIfPossible(componentType);
            }

            storages[count++] = _worldBuilder.ComponentStorageFactory.Factories[componentType](_worldBuilder);
        }

        var id = _worldBuilder.NextArchetypeId;
        configuredArchetype = new Archetype(id, storages);
        newArchetypeRegistered = _worldBuilder.TryRegisterArchetype(configuredArchetype, out configuredArchetype);
        Result = configuredArchetype;
        return _worldBuilder;
    }
}