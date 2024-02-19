namespace Deepslate.Ecs;

public sealed partial class WorldBuilder
{
    private readonly List<Stage> _stages = [];
    
    public IComponentDataPoolFactory ComponentPoolFactory { get; set; } = new DefaultComponentPoolFactory();
    public IReadOnlyList<Stage> Stages => _stages;
    
    public World? Result { get; private set; }

    public WorldBuilder WithComponentPoolFactory(IComponentDataPoolFactory componentPoolFactory)
    {
        ComponentPoolFactory = componentPoolFactory;
        return this;
    }
    
    public StageBuilder AddStage() => new(this);
    internal void RegisterStage(int index, Stage stage) => _stages.Insert(index, stage);

    public World Build()
    {
        if (Result is not null)
        {
            return Result;
        }
        Result = new World(_componentTypes, _archetypes, _stages, _storageArrayFactory);
        return Result;
    }
}