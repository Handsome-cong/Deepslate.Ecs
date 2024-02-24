using Deepslate.Ecs.Benchmark.Flecs.Components;

namespace Deepslate.Ecs.Benchmark.Flecs;

public sealed class FlecsApplication
{
    private global::Flecs.NET.Core.World _world;

    public int EntityCount { get; set; }

    public FlecsApplication()
    {
        _world = global::Flecs.NET.Core.World.Create();
    }

    public void Prepare()
    {
        foreach (var i in Enumerable.Range(0, EntityCount))
        {
            _world.Entity()
                .Set(new Position());
        }
    }

    public void Start()
    {
        _world.Each((ref Position position) =>
        {
            position.X += 1;
            position.Y += 1;
            position.Z += 1;
        } );
    }
}