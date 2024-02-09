namespace Deepslate.Ecs;

public sealed class Stage
{
    internal DependencyGraph Graph { get; }
    internal IReadOnlyList<DependencyGraphNode> StartupNodes { get; }

    internal Stage(DependencyGraph graph)
    {
        Graph = graph;
        StartupNodes = CollectStartupNodes(graph);
    }

    private static List<DependencyGraphNode> CollectStartupNodes(DependencyGraph graph)
    {
        var startupNodes = new List<DependencyGraphNode>();
        var nodes = graph.Nodes;

        for (var i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            if (node.OtherNodesThisDependsOn.Count > 0)
            {
                continue;
            }

            if (startupNodes.Any(startupNode => startupNode.OtherNodesConflictWithThis.Contains(node)))
            {
                continue;
            }
            startupNodes.Add(node);
        }

        return startupNodes;
    }
}