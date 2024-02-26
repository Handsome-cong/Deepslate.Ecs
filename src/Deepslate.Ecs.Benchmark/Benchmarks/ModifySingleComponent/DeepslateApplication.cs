using Deepslate.Ecs.Benchmark.Deepslate.Components;
using Deepslate.Ecs.Extensions;
using MovementSystem = Deepslate.Ecs.Benchmark.Deepslate.TickSystems.MovementSystem;

namespace Deepslate.Ecs.Benchmark.Benchmarks.ModifySingleComponent;

public sealed class DeepslateApplication : IDisposable
{
    private readonly World _world;
    private readonly Archetype _positionArchetype;
    
    public int EntityCount { get; set; } = 0;

    public DeepslateApplication()
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

    public void Dispose()
    {
        _world.Dispose();
    }
}