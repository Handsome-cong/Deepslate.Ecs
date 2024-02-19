namespace Deepslate.Ecs;

/// <summary>
/// A component used to indicate that the query requires instant command.
/// The id of this should be 0 in are cases.
/// This is designed to help processing dependencies.
/// </summary>
internal readonly record struct InstantCommandComponent : IComponentData;