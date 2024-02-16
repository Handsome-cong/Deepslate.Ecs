namespace Deepslate.Ecs;

public sealed class TickSystemBuilder
{
    private readonly List<Query> _queries = [];
    private readonly HashSet<TickSystem> _dependencies = [];

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

    public QueryBuilder AddQuery() => new(this);

    internal void RegisterQuery(Query query) => _queries.Add(query);

    public StageBuilder Build(ITickSystemExecutor executor, out TickSystem configuredTickSystem)
    {
        if (Result is not null)
        {
            configuredTickSystem = Result;
            return StageBuilder;
        }

        var usageCodes = CalculateUsageCodes();
        configuredTickSystem = new TickSystem(executor, _queries, _dependencies, usageCodes);
        Result = configuredTickSystem;
        StageBuilder.RegisterTickSystem(configuredTickSystem);
        return StageBuilder;
    }


    private UsageCode[] CalculateUsageCodes()
    {
        var worldBuilder = StageBuilder.WorldBuilder;
        var allArchetypeCount = worldBuilder.Archetypes.Count;
        var allComponentTypeCount = worldBuilder.ComponentTypes.Count;
        var archetypeUsageCodeCount = (allArchetypeCount - 1) / UsageCode.SizeOfBits + 1;
        var componentUsageCodeCount = (allComponentTypeCount - 1) / UsageCode.SizeOfBits + 1;
        var queryUsageCodeCount = archetypeUsageCodeCount + componentUsageCodeCount * 2;
        var queryCount = _queries.Count;
        var usageCodes = new UsageCode[queryUsageCodeCount * queryCount];
        for (var queryIndex = 0; queryIndex < queryCount; queryIndex++)
        {
            var query = _queries[queryIndex];
            var queryUsageCode = usageCodes.AsSpan().Slice(queryIndex * queryUsageCodeCount, queryUsageCodeCount);
            var archetypeUsageCodes = queryUsageCode[..archetypeUsageCodeCount];
            var writableComponentUsageCode = queryUsageCode[
                archetypeUsageCodeCount..(archetypeUsageCodeCount + componentUsageCodeCount)];
            var readableComponentUsageCode = queryUsageCode[(archetypeUsageCodeCount + componentUsageCodeCount)..];
            foreach (var archetype in query.MatchedArchetypes)
            {
                var archetypeId = archetype.Id;
                ref var archetypesUsageCode = ref archetypeUsageCodes[archetypeId / UsageCode.SizeOfBits];
                archetypesUsageCode = archetypesUsageCode.WithFlagOffset(archetypeId % UsageCode.SizeOfBits);
            }

            FillComponentUsageCode(writableComponentUsageCode, query.RequiredWritableComponentTypes);
            FillComponentUsageCode(readableComponentUsageCode, query.RequiredWritableComponentTypes);
            FillComponentUsageCode(readableComponentUsageCode, query.RequiredReadOnlyComponentTypes);

            if (query.RequireInstantArchetypeCommand)
            {
                var allComponentTypes = query.MatchedArchetypes
                    .SelectMany(archetype => archetype.ComponentTypes)
                    .ToHashSet();
                FillComponentUsageCode(writableComponentUsageCode, allComponentTypes);
                FillComponentUsageCode(readableComponentUsageCode, allComponentTypes);
            }
        }

        return usageCodes;
    }

    private void FillComponentUsageCode(
        Span<UsageCode> componentUsageCodes,
        IEnumerable<Type> componentTypes)
    {
        foreach (var componentType in componentTypes)
        {
            var componentTypeId = StageBuilder.WorldBuilder.ComponentTypeIds[componentType];
            ref var componentUsageCode = ref componentUsageCodes[componentTypeId / UsageCode.SizeOfBits];
            componentUsageCode = componentUsageCode.WithFlagOffset(componentTypeId % UsageCode.SizeOfBits);
        }
    }
}