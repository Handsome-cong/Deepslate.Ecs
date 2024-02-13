using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class MultiQuerySystem : ITickSystemExecutor
{
    [RequireWritable<Velocity>]
    [RequireReadOnly<Position>]
    private Query _query1;

    [RequireInstantCommand]
    [RequireWritable<Name>]
    private Query _query2;

    public MultiQuerySystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
    }
    
    public void Execute()
    {
        throw new NotImplementedException();
    }
}