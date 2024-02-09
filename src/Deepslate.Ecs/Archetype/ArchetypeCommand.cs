using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

/// <summary>
/// A wrapper of <see cref="Archetype"/> that provides a set of operations which can be executed immediately.
/// </summary>
/// <seealso cref="DeferredArchetypeCommand"/>
public readonly struct InstantArchetypeCommand : IArchetypeCommand<InstantArchetypeCommand>
{
    /// <summary>
    /// The <see cref="Archetype"/> that this command does operations on.
    /// </summary>
    public Archetype Archetype { get; }

    internal InstantArchetypeCommand(Archetype archetype)
    {
        Archetype = archetype;
    }

    /// <summary>
    /// Create an <see cref="Entity"/> in this <see cref="Archetype"/>.
    /// </summary>
    /// <returns>The created <see cref="Entity"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create() => Archetype.Create();

    /// <summary>
    /// Destroy the <see cref="Entity"/> from this <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">
    /// The entity you want to destroy.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="entity"/> destroyed successfully.
    /// May return <see langword="false"/> if the <paramref name="entity"/> does not exist.
    /// </returns>
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
    /// <seealso cref="Archetype.ContainsComponent{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Entity entity) => Archetype.ContainsEntity(entity);

    /// <summary>
    /// Get the component of the entity from this <see cref="Archetype"/>.
    /// This function can be used to modify the component.
    /// </summary>
    /// <param name="entity">
    /// The entity whose component you want to get.
    /// </param>
    /// <typeparam name="TComponent">
    /// The type of the component you want to get.
    /// </typeparam>
    /// <returns>
    /// A <see cref="Span{T}"/> with length 1 that references the component,
    /// or an empty <see cref="Span{T}"/> if the entity or the component does not exist.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <typeparamref name="TComponent"/> does not exist in this archetype.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref TComponent GetComponent<TComponent>(Entity entity)
        where TComponent : IComponentData => ref Archetype.GetComponent<TComponent>(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static InstantArchetypeCommand IArchetypeCommand<InstantArchetypeCommand>.Create(Archetype archetype) =>
        new(archetype);
}

/// <summary>
/// A wrapper of <see cref="Archetype"/> that provides a set of operations which can be deferred.
/// </summary>
/// <seealso cref="InstantArchetypeCommand"/>
public readonly struct DeferredArchetypeCommand : IArchetypeCommand<DeferredArchetypeCommand>
{
    /// <inheritdoc cref="InstantArchetypeCommand.Archetype"/>
    public Archetype Archetype { get; }

    internal DeferredArchetypeCommand(Archetype archetype)
    {
        Archetype = archetype;
    }

    /// <summary>
    /// Enqueue a create operation which will be deferred.
    /// </summary>
    /// <param name="initializer">
    /// <para>
    /// A handler that will be invoked after the entity is created.
    /// </para>
    /// </param>
    /// <param name="timing">Indicates when the command will be executed.</param>
    /// <returns>The entity that will be created.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeferredCreate(
        Action<EntityComponentAccessor>? initializer = null,
        ExecuteTiming timing = ExecuteTiming.EndOfStage) =>
        Archetype.AddCreationCommand(initializer, timing);

    /// <summary>
    /// Enqueue a destroy operation which will be deferred.
    /// </summary>
    /// <param name="entity">The entity to destroy.</param>
    /// <param name="finalizer">
    /// <para>
    /// A handler that will be invoked before the entity is destroyed.
    /// The <see cref="EntityComponentAccessor.Alive"/> property of the
    /// <see cref="EntityComponentAccessor"/> parameter will be <see langword="false"/>,
    /// if the entity was destroyed before this handler is invoked.
    /// </para>
    /// </param>
    /// <param name="timing">Indicates when the command will be executed.</param>
    /// <returns><see langword="true"/> if the entity exists, otherwise <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool DeferredDestroy(
        Entity entity,
        Action<EntityComponentAccessor>? finalizer = null,
        ExecuteTiming timing = ExecuteTiming.EndOfStage)
    {
        if (!Archetype.ContainsEntity(entity))
        {
            return false;
        }
        Archetype.AddDestructionCommand(entity, finalizer, timing);
        return true;
    }

    /// <inheritdoc cref="InstantArchetypeCommand.Contains"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Entity entity) => Archetype.ContainsEntity(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static DeferredArchetypeCommand IArchetypeCommand<DeferredArchetypeCommand>.Create(Archetype archetype) =>
        new(archetype);

    /// <summary>
    /// When the deferred command will be executed.
    /// </summary>
    public enum ExecuteTiming
    {
        /// <summary>
        /// Execute the command at the end of the current stage.
        /// </summary>
        EndOfStage,

        /// <summary>
        /// Execute the command at the end of the current tick, after all <see cref="EndOfStage"/> commands.
        /// </summary>
        EndOfTick,
    }
}

// ReSharper disable once TypeParameterCanBeVariant
public interface IArchetypeCommand<TSelf>
    where TSelf : struct, IArchetypeCommand<TSelf>
{
    public Archetype Archetype { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static abstract TSelf Create(Archetype archetype);
}

public enum ArchetypeCommandType
{
    None,
    Instant,
    Deferred
}