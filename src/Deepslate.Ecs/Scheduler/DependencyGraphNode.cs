namespace Deepslate.Ecs;

internal sealed class DependencyGraphNode(int id, TickSystem tickSystem)
{
    internal readonly int Id = id;
    public TickSystem TickSystem { get; } = tickSystem;

    private readonly HashSet<DependencyGraphNode> _otherNodesThisDependsOn = [];
    private readonly HashSet<DependencyGraphNode> _otherNodesDependOnThis = [];
    private readonly HashSet<DependencyGraphNode> _otherNodesConflictWithThis = [];

    internal IReadOnlySet<DependencyGraphNode> OtherNodesThisDependsOn => _otherNodesThisDependsOn;
    internal IReadOnlySet<DependencyGraphNode> OtherNodesDependOnThis => _otherNodesDependOnThis;
    internal IReadOnlySet<DependencyGraphNode> OtherNodesConflictWithThis => _otherNodesConflictWithThis;


    public bool LinkIfRelevant(DependencyGraphNode other, int allArchetypeCount, int allComponentTypeCount)
    {
        if (other == this)
        {
            return false;
        }

        if (TickSystem.OtherSystemsThisDependsOn.Contains(other.TickSystem))
        {
            _otherNodesThisDependsOn.Add(other);
            other._otherNodesDependOnThis.Add(this);
            return true;
        }

        if (other.TickSystem.OtherSystemsThisDependsOn.Contains(TickSystem))
        {
            _otherNodesDependOnThis.Add(other);
            other._otherNodesThisDependsOn.Add(this);
            return true;
        }

        var selfUsageCodeBundle = new UsageCodeBundle(TickSystem.UsageCodes, TickSystem.InstantCommandFlags,
            allArchetypeCount, allComponentTypeCount);
        var otherUsageCodeBundle = new UsageCodeBundle(other.TickSystem.UsageCodes, TickSystem.InstantCommandFlags,
            allArchetypeCount, allComponentTypeCount);
        if (selfUsageCodeBundle.ConflictWith(otherUsageCodeBundle))
        {
            _otherNodesConflictWithThis.Add(other);
            other._otherNodesConflictWithThis.Add(this);
        }
        else
        {
            return false;
        }

        return true;
    }
}