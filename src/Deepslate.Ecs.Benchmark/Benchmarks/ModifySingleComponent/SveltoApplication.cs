using Deepslate.Ecs.Benchmark.Svelto.Components;
using Deepslate.Ecs.Benchmark.Svelto.Engines;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

namespace Deepslate.Ecs.Benchmark.Benchmarks.ModifySingleComponent;

public sealed class SveltoApplication : IDisposable
{
    private EnginesRoot _enginesRoot;
    private EntitiesSubmissionScheduler _entitiesSubmissionScheduler;

    private ExclusiveGroup _group;
    
    private MovementEngine _movementEngine;
    
    public int EntityCount { get; set; }
    
    public SveltoApplication()
    {
        _entitiesSubmissionScheduler = new EntitiesSubmissionScheduler();
        _enginesRoot = new EnginesRoot(_entitiesSubmissionScheduler);

        _group = new ExclusiveGroup();
        _movementEngine = new MovementEngine(_group);
        _enginesRoot.AddEngine(_movementEngine);
    }

    public void Prepare()
    {
        var entityFactory = _enginesRoot.GenerateEntityFactory();
        foreach (var i in Enumerable.Range(0, EntityCount))
        {
            entityFactory.BuildEntity<PositionDescriptor>(new EGID((uint)i, _group));
        }
        _entitiesSubmissionScheduler.SubmitEntities();
    }
    
    public void Start()
    {
        _movementEngine.Update();
    }

    public void Dispose()
    {
        _enginesRoot.Dispose();
    }
}