using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class CreateSystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [RequireInstantCommand]
    private Query _query;

    private readonly int _count;

    public CreateSystem(TickSystemBuilder builder, int count)
    {
        InitializeQuery(builder);
        builder.Build(this, out _);
        _count = count;
    }

    public void Execute(EntityCommand command)
    {
        foreach (var _ in Enumerable.Range(0, _count))
        {
            command.Create(_query.MatchedArchetypes[0]);
        }
    }
}