using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class MultiQuerySystem : ITickSystemExecutor
{
    [WithWritable<Velocity>] [WithReadOnly<Position>]
    private Query _query1;

    [RequireInstantCommand]
    [WithWritable<Name>]
    [AsGenericQuery(useProperty: true, memberName: "Query2", modifier: GeneratedGenericQueryAccessModifier.Public)]
    private Query _query2;

    public MultiQuerySystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
    }

    public void Execute(Command command)
    {
    }
}