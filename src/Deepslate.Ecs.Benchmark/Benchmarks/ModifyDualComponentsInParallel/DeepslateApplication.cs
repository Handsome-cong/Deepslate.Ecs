using Deepslate.Ecs.Benchmark.Benchmarks.Common;
using Deepslate.Ecs.Benchmark.Deepslate.Components;
using Deepslate.Ecs.Benchmark.Deepslate.TickSystems;
using Deepslate.Ecs.Extensions;

namespace Deepslate.Ecs.Benchmark.Benchmarks.ModifyDualComponentsInParallel;

public sealed class DeepslateApplication : IBenchmarkApplication
{
    private readonly World _world;
    private readonly Archetype _positionArchetype;

    public int EntityCount { get; set; }

    public DeepslateApplication()
    {
        _world = new WorldBuilder()
            .WithArchetypeAndBuild<Position, Velocity>(out _positionArchetype)
            .AddStageAndBuild(stageBuilder => stageBuilder
                .AddTickSystem(tickSystemBuilder => tickSystemBuilder.Build(new MovementSystem(tickSystemBuilder)))
                .AddTickSystem(tickSystemBuilder => tickSystemBuilder.Build(new AccelerationSystem(tickSystemBuilder))))
            .Build();
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