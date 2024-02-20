using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class DestroySystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [RequireInstantCommand] [AsGenericQuery]
    private Query _query;

    public DestroySystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
        builder.Build(this, out _);
    }

    public void Execute(Command command)
    {
        List<Entity> entities = [];
        foreach (var entityComponentBundle in _queryGeneric)
        {
            entities.Add(entityComponentBundle.Entity);
        }

        foreach (var entity in entities)
        {
            command.Destroy(entity);
        }
    }
}