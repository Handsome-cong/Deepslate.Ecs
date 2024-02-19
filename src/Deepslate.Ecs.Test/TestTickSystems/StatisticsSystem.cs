using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class StatisticsSystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [AsGenericQuery]
    private Query _query;
    
    public int Count { get; private set; }
    
    public StatisticsSystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
    }
    
    public void Execute(EntityCommand command)
    {
        Count = 0;
        foreach (var entityComponentBundle in _queryGeneric)
        {
            Count++;
        }
    }
}