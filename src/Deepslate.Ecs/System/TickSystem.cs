using System.Collections.Frozen;

namespace Deepslate.Ecs;

public sealed class TickSystem
{
    private readonly HashSet<TickSystem> _otherSystemsThisDependsOn;
    private readonly UsageCode[] _usageCodes;
    
    internal Task ExecutionTask = Task.CompletedTask;
    
    internal IReadOnlySet<TickSystem> OtherSystemsThisDependsOn => _otherSystemsThisDependsOn;
    internal ReadOnlySpan<UsageCode> UsageCodes => _usageCodes;
    
    public ITickSystemExecutor Executor { get; }
    public bool Running => !ExecutionTask.IsCompleted;
    public FrozenSet<Query> Queries { get; }

    internal TickSystem(
        ITickSystemExecutor executor,
        IEnumerable<Query> queries,
        IEnumerable<TickSystem> dependentSystems,
        IEnumerable<UsageCode> usageCodes)
    {
        Executor = executor;
        Queries = queries.ToFrozenSet();
        _otherSystemsThisDependsOn = [..dependentSystems];

        _usageCodes = usageCodes.ToArray();
    }
}