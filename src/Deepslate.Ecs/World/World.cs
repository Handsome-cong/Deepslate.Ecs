using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed class World : IDisposable
{
    private readonly Archetype[] _archetypes;
    private readonly Type[] _componentTypes;
    private readonly Stage[] _stages;


    internal readonly ParallelScheduler Scheduler;
    internal readonly FrozenDictionary<Type, FrozenSet<ushort>> ComponentTypeToArchetypeIds;
    internal readonly FrozenDictionary<Type, int> ComponentTypeIds;
    internal readonly FrozenDictionary<Type, Delegate> ResourceFactories;
    internal readonly FrozenDictionary<Type, int> ResourceIds;

    internal readonly StorageArrayFactory StorageArrayFactory;

    public IReadOnlyList<Archetype> Archetypes => _archetypes;
    public IReadOnlyList<Type> ComponentTypes => _componentTypes;
    public IReadOnlyList<Stage> Stages => _stages;
    public FrozenDictionary<Type, FrozenSet<Archetype>> ComponentTypeToArchetypes { get; }

    internal World(
        IEnumerable<Type> components,
        IEnumerable<Archetype> archetypes,
        IEnumerable<Stage> stages,
        IDictionary<Type, Delegate> resourceFactories,
        StorageArrayFactory storageArrayFactory)
    {
        _componentTypes = components
            .OrderBy(component => component.GetHashCode())
            .ToArray();
        _archetypes = archetypes.OrderBy(archetype => archetype.Id).ToArray();
        _stages = stages.ToArray();
        ResourceFactories = resourceFactories.ToFrozenDictionary();
        ResourceIds = resourceFactories
            .Select((type, id) => (type.Key, id))
            .ToFrozenDictionary(tuple => tuple.Key, tuple => tuple.id);
        StorageArrayFactory = storageArrayFactory;

        ComponentTypeToArchetypeIds = _archetypes
            .SelectMany(archetype => archetype.ComponentTypes.Select(type => (type, archetype.Id)))
            .GroupBy(tuple => tuple.type)
            .ToFrozenDictionary(group => group.Key, group => group.Select(tuple => tuple.Id).ToFrozenSet());

        ComponentTypeToArchetypes = _archetypes
            .SelectMany(archetype => archetype.ComponentTypes.Select(type => (type, archetype)))
            .GroupBy(tuple => tuple.type)
            .ToFrozenDictionary(group => group.Key, group => group.Select(tuple => tuple.archetype).ToFrozenSet());

        ComponentTypeIds = _componentTypes
            .Select((type, index) => (type, index))
            .ToFrozenDictionary(tuple => tuple.type, tuple => tuple.index);

        Scheduler = new ParallelScheduler(this);

        foreach (var stage in _stages)
        {
            stage.PostInitialize(this);
        }
    }

    public void Tick() => Scheduler.Tick();

    public Task TickAsync() => Scheduler.TickAsync();

    public GlobalCommand CreateGlobalCommand()
    {
        return new GlobalCommand(this);
    }

    public void Dispose()
    {
        foreach (var archetype in _archetypes)
        {
            archetype.Dispose();
        }
    }
}