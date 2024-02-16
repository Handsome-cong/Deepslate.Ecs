namespace Deepslate.Ecs;

public sealed class QueryResultAccessor
{
    public TickSystem TickSystem { get; }

    internal QueryResultAccessor(TickSystem system)
    {
        TickSystem = system;
    }
    
    
}