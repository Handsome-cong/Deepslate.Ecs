namespace Deepslate.Ecs;

internal sealed class DependencyGraph
{
    private readonly DependencyGraphNode[] _nodes;
    
    internal IReadOnlyList<DependencyGraphNode> Nodes => _nodes;

    internal DependencyGraph(StageBuilder stageBuilder)
    {
        var tickSystemsArray = stageBuilder.TickSystems.ToArray();
        
        _nodes = new DependencyGraphNode[tickSystemsArray.Length];
        for (var i = 0; i < tickSystemsArray.Length; i++)
        {
            _nodes[i] = new DependencyGraphNode(i, tickSystemsArray[i]);
        }
        
        LinkNodes(stageBuilder);
    }

    private void LinkNodes(StageBuilder stageBuilder)
    {
        // A brute force approach to link all nodes.
        // There should be room for optimization here, if the graph is big enough.
        
        var allArchetypeCount = stageBuilder.WorldBuilder.Archetypes.Count;
        var allComponentTypeCount = stageBuilder.WorldBuilder.ComponentTypes.Count;
        
        for (var i = 0; i < _nodes.Length; i++)
        {
            for (var j = i + 1; j < _nodes.Length; j++)
            {
                _nodes[i].LinkIfRelevant(_nodes[j], allArchetypeCount, allComponentTypeCount);
            }
        }
    }
}