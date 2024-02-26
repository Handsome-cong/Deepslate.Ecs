using Deepslate.Ecs.Benchmark.Benchmarks.Common;
using Deepslate.Ecs.Benchmark.Deepslate.Components;
using Deepslate.Ecs.Extensions;

namespace Deepslate.Ecs.Benchmark.Benchmarks.CreateEntity;

public sealed class DeepslateApplication : IBenchmarkApplication
{
    private readonly World _world;
    private readonly Archetype _positionArchetype;
    
    public int EntityCount { get; set; } = 0;

    public DeepslateApplication()
    {
        _world = new WorldBuilder()
            .WithArchetypeAndBuild<Position>(out _positionArchetype)
            .Build();
    }
    
    public void Prepare()
    {
    }

    public void Start()
    {
        var command = _world.CreateGlobalCommand();
        command.CreateMany(_positionArchetype, EntityCount);
    }

    public void Dispose()
    {
        _world.Dispose();
    }
}