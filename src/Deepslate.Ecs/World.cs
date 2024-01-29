using System.Collections.Frozen;
using Deepslate.Ecs;

namespace Deepslate.Ecs;

public sealed class World : IDisposable
{
    internal FrozenDictionary<Type, int[]> ComponentTypeToArchetypeIndices { get; }
    private Archetype[] _archetypes;
    private ISystem[] _systems;

    public IReadOnlyList<Archetype> Archetypes => _archetypes;

    internal World(
        IEnumerable<Archetype> archetype,
        IEnumerable<ISystem> systems)
    {
        _archetypes = archetype.ToArray();

        ComponentTypeToArchetypeIndices = _archetypes
            .SelectMany((x, index) => x.ComponentTypes.Select(x => (x, index)))
            .GroupBy(tuple => tuple.x, tuple => tuple.index)
            .ToFrozenDictionary(group => group.Key, group => group.ToArray());
        
        _systems = systems.ToArray();
    }

    public void Dispose()
    {
        foreach (var archetype in _archetypes)
        {
            archetype.Dispose();
        }
    }
}
