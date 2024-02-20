namespace Deepslate.Ecs;

public sealed class TickSystemBuilder
{
    private readonly List<Query> _queries = [];
    private readonly HashSet<TickSystem> _dependencies = [];
    private readonly HashSet<Type> _resourceTypes = [];

    public StageBuilder StageBuilder { get; }
    public TickSystem? Result { get; private set; }
    public IEnumerable<Query> Queries => _queries;
    public IEnumerable<TickSystem> Dependencies => _dependencies;

    internal TickSystemBuilder(StageBuilder stageBuilder)
    {
        StageBuilder = stageBuilder;
    }

    public TickSystemBuilder WithDependency(TickSystem system)
    {
        _dependencies.Add(system);
        return this;
    }
    
    public TickSystemBuilder WithResource<TResource>()
        where TResource : IResource
    {
        _resourceTypes.Add(typeof(TResource));
        return this;
    }

    public QueryBuilder AddQuery() => new(this);

    internal void RegisterQuery(Query query) => _queries.Add(query);

    public StageBuilder Build(ITickSystemExecutor executor, out TickSystem configuredTickSystem)
    {
        if (Result is not null)
        {
            configuredTickSystem = Result;
            return StageBuilder;
        }

        configuredTickSystem = new TickSystem(executor, _queries, _dependencies, _resourceTypes);
        Result = configuredTickSystem;
        StageBuilder.RegisterTickSystem(configuredTickSystem);
        return StageBuilder;
    }
}