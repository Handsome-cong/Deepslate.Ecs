using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class DeferCreateSystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [RequireInstantCommand]
    private Query _query;

    private readonly int _count;

    public DeferCreateSystem(TickSystemBuilder builder, int count)
    {
        InitializeQuery(builder);
        builder.Build(this, out _);
        _count = count;
    }

    public void Execute(EntityCommand command)
    {
        command.DeferCreate(_query.MatchedArchetypes[0], count: _count);
    }
}