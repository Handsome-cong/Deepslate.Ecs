namespace Deepslate.Ecs;

public sealed class Stage
{
    private readonly IReadOnlyList<TickSystem> _tickSystems;

    internal DependencyGraph Graph { get; private set; } = default!;

    internal ParallelScheduler Scheduler { get; private set; } = default!;

    internal Stage(IReadOnlyList<TickSystem> tickSystems)
    {
        _tickSystems = tickSystems;
    }

    internal void PostInitialize(World world)
    {
        Scheduler = world.Scheduler;
        foreach (var tickSystem in _tickSystems)
        {
            tickSystem.PostInitialize(world, this);
        }
        Graph = new DependencyGraph(_tickSystems, world.Archetypes.Count, world.ComponentTypes.Count);
    }
}