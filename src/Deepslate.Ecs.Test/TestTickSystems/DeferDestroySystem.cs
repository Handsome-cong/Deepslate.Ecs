using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;


public sealed partial class DeferDestroySystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [AsGenericQuery]
    private Query _query;

    public DeferDestroySystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
        builder.Build(this, out _);
    }

    public void Execute(EntityCommand command)
    {
        List<Entity> entities = [];
        foreach (var entityComponentBundle in _queryGeneric)
        {
            entities.Add(entityComponentBundle.Entity);
        }

        command.DeferDestroy(entities);
    }
}