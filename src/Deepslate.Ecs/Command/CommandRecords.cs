namespace Deepslate.Ecs;

internal readonly record struct CreationCommandRecord(
    int Count,
    Action<EntityComponentAccessor>? Initializer);

internal readonly record struct DestructionCommandRecord(
    IEnumerable<Entity> Entities,
    Action<EntityComponentAccessor>? Finalizer);