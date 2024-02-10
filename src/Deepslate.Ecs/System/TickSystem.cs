namespace Deepslate.Ecs;

public sealed class TickSystem
{
    private readonly Query[] _queries;

    private readonly HashSet<TickSystem> _otherSystemsThisDependsOn;
    private readonly UsageCode[] _usageCodes;
    
    internal IReadOnlySet<TickSystem> OtherSystemsThisDependsOn => _otherSystemsThisDependsOn;
    internal ReadOnlySpan<UsageCode> UsageCodes => _usageCodes;
    
    public ITickSystemExecutor Executor { get; }

    internal TickSystem(
        ITickSystemExecutor executor,
        IEnumerable<Query> queries,
        IEnumerable<TickSystem> dependentSystems,
        IEnumerable<UsageCode> usageCodes)
    {
        Executor = executor;
        _queries = queries.ToArray();
        _otherSystemsThisDependsOn = [..dependentSystems];

        _usageCodes = usageCodes.ToArray();
    }
}