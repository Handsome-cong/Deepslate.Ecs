namespace Deepslate.Ecs;

public sealed class StageBuilder
{
    private readonly List<TickSystem> _tickSystems = [];

    public WorldBuilder WorldBuilder { get; }
    public Stage? Result { get; private set; }
    public IReadOnlyList<TickSystem> TickSystems => _tickSystems;


    internal StageBuilder(WorldBuilder worldBuilder)
    {
        WorldBuilder = worldBuilder;
    }

    public TickSystemBuilder AddTickSystem() => new(this);
    internal void RegisterTickSystem(TickSystem tickSystem) => _tickSystems.Add(tickSystem);

    public WorldBuilder Build(int index, out Stage configuredStage)
    {
        if (Result is not null)
        {
            configuredStage = Result;
            return WorldBuilder;
        }
        
        var graph = new DependencyGraph(this);
        configuredStage = new Stage(graph);
        Result = configuredStage;
        WorldBuilder.RegisterStage(index, configuredStage);
        return WorldBuilder;
    }
}