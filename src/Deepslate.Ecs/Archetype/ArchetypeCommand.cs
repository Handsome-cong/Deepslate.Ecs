using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

/// <summary>
/// A wrapper of <see cref="Archetype"/> that allows operations with structural changes.
/// </summary>
public readonly struct WritableArchetypeCommand : IArchetypeCommand<WritableArchetypeCommand>
{
    /// <summary>
    /// The archetype that this command does operations on.
    /// </summary>
    public Archetype Archetype { get; }

    internal WritableArchetypeCommand(Archetype archetype)
    {
        Archetype = archetype;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create() => Archetype.Create();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Destroy(Entity entity) => Archetype.Destroy(entity);

    /// <summary>
    /// Check if the archetype contains the entity.
    /// </summary>
    /// <param name="entity">
    /// The entity to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the archetype contains the entity, otherwise <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Entity entity) => Archetype.Contains(entity);

    /// <summary>
    /// Get the component of the entity from this <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">
    /// The entity whose component you want to get.
    /// </param>
    /// <typeparam name="TComponent">
    /// The type of the component you want to get.
    /// </typeparam>
    /// <returns>
    /// The component reference of the entity.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the entity or the component does not exist in this <see cref="Archetype"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent : IComponent => ref Archetype.GetComponent<TComponent>(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static WritableArchetypeCommand IArchetypeCommand<WritableArchetypeCommand>.Create(Archetype archetype) =>
        new(archetype);
}

/// <summary>
/// A wrapper of <see cref="Archetype"/> that allows operations without structural changes.
/// </summary>
public readonly struct ReadOnlyArchetypeCommand : IArchetypeCommand<ReadOnlyArchetypeCommand>
{
    /// <inheritdoc cref="WritableArchetypeCommand.Archetype"/>
    public Archetype Archetype { get; }

    internal ReadOnlyArchetypeCommand(Archetype archetype)
    {
        Archetype = archetype;
    }

    /// <inheritdoc cref="WritableArchetypeCommand.Contains"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Entity entity) => Archetype.Contains(entity);

    /// <inheritdoc cref="WritableArchetypeCommand.GetComponent{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent : IComponent => ref Archetype.GetComponent<TComponent>(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static ReadOnlyArchetypeCommand IArchetypeCommand<ReadOnlyArchetypeCommand>.Create(Archetype archetype) =>
        new(archetype);
}

public interface IArchetypeCommand<out TSelf>
    where TSelf : struct, IArchetypeCommand<TSelf>
{
    public Archetype Archetype { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static abstract TSelf Create(Archetype archetype);
}
