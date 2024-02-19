namespace Deepslate.Ecs;

internal sealed class DependencyGraph
{
    private readonly DependencyGraphNode[] _nodes;

    internal IReadOnlyList<DependencyGraphNode> Nodes => _nodes;

    internal DependencyGraph(
        IReadOnlyList<TickSystem> tickSystems,
        int allArchetypeCount,
        int allComponentTypeCount)
    {
        _nodes = new DependencyGraphNode[tickSystems.Count];
        for (var i = 0; i < tickSystems.Count; i++)
        {
            _nodes[i] = new DependencyGraphNode(i, tickSystems[i]);
        }

        LinkNodes(allArchetypeCount, allComponentTypeCount);
    }

    private void LinkNodes(int allArchetypeCount, int allComponentTypeCount)
    {
        // A brute force approach to link all nodes.
        // There should be room for optimization here, if the graph is big enough.

        for (var i = 0; i < _nodes.Length; i++)
        {
            for (var j = i + 1; j < _nodes.Length; j++)
            {
                _nodes[i].LinkIfRelevant(_nodes[j], allArchetypeCount, allComponentTypeCount);
            }
        }
    }
}