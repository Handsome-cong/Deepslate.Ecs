using Deepslate.Ecs.Benchmark.Ecs.Components;
using Deepslate.Ecs.Benchmark.Ecs.TickSystems;
using Deepslate.Ecs.Extensions;

namespace Deepslate.Ecs.Benchmark.Ecs;

public sealed class EcsApplication
{
    private readonly World _world;
    private readonly Archetype _positionArchetype;
    
    public int EntityCount { get; set; } = 0;

    public EcsApplication()
    {
        _world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out _positionArchetype)
            .AddStageAndBuild(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                    tickSystemBuilder.Build(new MovementSystem(tickSystemBuilder)));
            }).Build();
    }
    
    public void Prepare()
    {
        var command = _world.CreateGlobalCommand();
        command.CreateMany(_positionArchetype, EntityCount);
    }

    public void Start()
    {
        _world.Tick();
    }
}