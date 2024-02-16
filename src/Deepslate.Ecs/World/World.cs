using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed class World : IDisposable
{
    private readonly Archetype[] _archetypes;
    private readonly Type[] _componentTypes;
    private readonly Stage[] _stages;

    private readonly ParallelScheduler _scheduler;
    
    public IReadOnlyList<Archetype> Archetypes => _archetypes;
    public IReadOnlyList<Type> ComponentTypes => _componentTypes;
    public IReadOnlyList<Stage> Stages => _stages;
    public FrozenDictionary<Type, FrozenSet<Archetype>> ComponentTypeToArchetypes { get; }

    internal World(
        IEnumerable<Type> components,
        IEnumerable<Archetype> archetypes,
        IEnumerable<Stage> stages)
    {
        _componentTypes = components.OrderBy(component => component.GetHashCode()).ToArray();
        _archetypes = archetypes.OrderBy(archetype => archetype.Id).ToArray();
        _stages = stages.ToArray();

        ComponentTypeToArchetypes = _archetypes
            .SelectMany(archetype => archetype.ComponentTypes.Select(type => (type, archetype)))
            .GroupBy(tuple => tuple.type)
            .ToFrozenDictionary(group => group.Key, group => group.Select(tuple => tuple.archetype).ToFrozenSet());

        _scheduler = new ParallelScheduler(_stages, _archetypes);
    }

    public void Tick() => _scheduler.Tick();

    public Task TickAsync() => _scheduler.TickAsync();

    public GlobalArchetypeCommand CreateGlobalArchetypeCommand()
    {
        return new GlobalArchetypeCommand(this);
    }

    public void Dispose()
    {
        foreach (var archetype in _archetypes)
        {
            archetype.Dispose();
        }
    }
}