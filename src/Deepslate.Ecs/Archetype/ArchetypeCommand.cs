using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

/// <summary>
/// <para>
/// You can use this to execute commands on an <see cref="Archetype"/> immediately,
/// or record commands in a <see cref="CommandBuffer"/> and execute them later manually.
/// </para>
/// <para>
/// If you want to execute commands after current <see cref="TickSystem"/>,
/// use <see cref="DeferredArchetypeCommand"/> instead.
/// </para>
/// </summary>
public readonly struct InstantArchetypeCommand
{
    /// <summary>
    /// The <see cref="Archetype"/> that this command does operations on.
    /// </summary>
    public Archetype Archetype { get; } = Archetype.Empty;

    public TickSystem TickSystem { get; }

    internal InstantArchetypeCommand(Archetype archetype, TickSystem tickSystem)
    {
        Archetype = archetype;
        TickSystem = tickSystem;
    }

    /// <summary>
    /// <para>
    /// Record a create operation in the buffer.
    /// </para>
    /// <para>
    /// This method is thread-safe.
    /// </para>
    /// </summary>
    /// <param name="buffer">
    /// The buffer to record the command.
    /// </param>
    /// <param name="initializer">
    /// An optional initializer that will be invoked after the entity is created
    /// and <see cref="IReactAfterCreate{TComponent}"/> is invoked.
    /// </param>
    /// <param name="count">
    /// The number of entities to create.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="Archetype"/> is empty.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RecordCreate(
        CommandBuffer buffer,
        Action<EntityComponentAccessor>? initializer = null,
        int count = 1)
    {
        ThrowIfArchetypeIsEmptyOrSystemIsNotRunning();
        buffer.AddCreationCommand(Archetype, new CreationCommandRecord(count, initializer));
    }

    /// <summary>
    /// <para>
    /// Record a destroy operation in the buffer.
    /// The operation will not be recorded if the entity does not exist in the <see cref="Archetype"/>.
    /// </para>
    /// <para>
    /// This method is thread-safe.
    /// </para>
    /// </summary>
    /// <param name="buffer">
    /// The buffer to record the command.
    /// </param>
    /// <param name="entities">
    /// The entities to destroy.
    /// </param>
    /// <param name="finalizer">
    /// An optional finalizer that will be invoked before the entity is destroyed
    /// and <see cref="IReactBeforeDestroy{TComponent}"/> is invoked.
    /// </param>
    /// <returns>
    /// The number of entities that will be destroyed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int RecordDestroy(
        CommandBuffer buffer,
        IEnumerable<Entity> entities,
        Action<EntityComponentAccessor>? finalizer = null)
    {
        ThrowIfArchetypeIsEmptyOrSystemIsNotRunning();
        var entitiesArray = entities.Where(Archetype.ContainsEntity).ToArray();
        if (entitiesArray.Length != 0)
        {
            buffer.AddDestructionCommand(Archetype, new DestructionCommandRecord(entitiesArray, finalizer));
        }

        return entitiesArray.Length;
    }

    /// <summary>
    /// Create an <see cref="Entity"/> in this <see cref="Archetype"/>.
    /// </summary>
    /// <returns>The created <see cref="Entity"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create()
    {
        ThrowIfArchetypeIsEmptyOrSystemIsNotRunning();
        return Archetype.Create();
    }

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
    public bool Destroy(Entity entity)
    {
        ThrowIfArchetypeIsEmptyOrSystemIsNotRunning();
        return Archetype.Destroy(entity);
    }

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
    public bool Contains(Entity entity)
    {
        ThrowIfArchetypeIsEmptyOrSystemIsNotRunning();
        return Archetype.ContainsEntity(entity);
    }

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
        where TComponent : IComponentData
    {
        ThrowIfArchetypeIsEmptyOrSystemIsNotRunning();
        return ref Archetype.GetComponent<TComponent>(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfArchetypeIsEmptyOrSystemIsNotRunning()
    {
        if (Archetype == Archetype.Empty)
        {
            throw new InvalidOperationException(
                "The archetype is empty. You should bind an archetype before using InstantArchetypeCommand.");
        }
    }
}

/// <summary>
/// <para>
/// You can use this to record commands on an <see cref="Archetype"/> and execute them at the end of stage or tick.
/// The recorded commands will be executed automatically.
/// </para>
/// <para>
/// If you want to execute commands immediately, use <see cref="InstantArchetypeCommand"/> instead.
/// </para>
/// </summary>
/// <seealso cref="InstantArchetypeCommand"/>
public readonly struct DeferredArchetypeCommand
{
    private readonly UncheckedCommandBuffer _commandBufferEndOfStage;
    private readonly UncheckedCommandBuffer _commandBufferEndOfTick;

    /// <inheritdoc cref="InstantArchetypeCommand.Archetype"/>
    public Archetype Archetype { get; }

    internal DeferredArchetypeCommand(
        Archetype archetype,
        UncheckedCommandBuffer commandBufferEndOfStage,
        UncheckedCommandBuffer commandBufferEndOfTick)
    {
        Archetype = archetype;
        _commandBufferEndOfStage = commandBufferEndOfStage;
        _commandBufferEndOfTick = commandBufferEndOfTick;
    }

    /// <summary>
    /// Enqueue a create operation which will be deferred.
    /// </summary>
    /// <param name="initializer">
    /// A handler that will be invoked after the entity is created.
    /// </param>
    /// <param name="count">
    /// The number of entities to create.
    /// </param>
    /// <param name="timePoint">
    /// Indicates when the command will be executed.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeferredCreate(
        Action<EntityComponentAccessor>? initializer = null,
        int count = 1,
        ExecuteTimePoint timePoint = ExecuteTimePoint.EndOfStage)
    {
        switch (timePoint)
        {
            case ExecuteTimePoint.EndOfStage:
                _commandBufferEndOfStage.AddCreationCommand(Archetype, new CreationCommandRecord(count, initializer));
                break;
            case ExecuteTimePoint.EndOfTick:
                _commandBufferEndOfTick.AddCreationCommand(Archetype, new CreationCommandRecord(count, initializer));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(timePoint), timePoint, "Invalid time point.");
        }
    }

    /// <summary>
    /// Enqueue a destroy operation which will be deferred.
    /// </summary>
    /// <param name="entities">
    /// The entities to destroy.
    /// </param>
    /// <param name="finalizer">
    /// A handler that will be invoked before the entity is destroyed.
    /// </param>
    /// <param name="timePoint">
    /// Indicates when the command will be executed.
    /// </param>
    /// <returns>
    /// The number of entities that will be destroyed.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int DeferredDestroy(
        IEnumerable<Entity> entities,
        Action<EntityComponentAccessor>? finalizer = null,
        ExecuteTimePoint timePoint = ExecuteTimePoint.EndOfStage)
    {
        var entitiesArray = entities.Where(Archetype.ContainsEntity).ToArray();
        if (entitiesArray.Length == 0)
        {
            return entitiesArray.Length;
        }

        switch (timePoint)
        {
            case ExecuteTimePoint.EndOfStage:
                _commandBufferEndOfStage.AddDestructionCommand(Archetype,
                    new DestructionCommandRecord(entitiesArray, finalizer));
                break;
            case ExecuteTimePoint.EndOfTick:
                _commandBufferEndOfTick.AddDestructionCommand(Archetype,
                    new DestructionCommandRecord(entitiesArray, finalizer));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(timePoint), timePoint, "Invalid time point.");
        }

        return entitiesArray.Length;
    }

    /// <inheritdoc cref="InstantArchetypeCommand.Contains"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Entity entity) => Archetype.ContainsEntity(entity);

    /// <summary>
    /// When the deferred command will be executed.
    /// </summary>
    public enum ExecuteTimePoint
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

internal readonly record struct CreationCommandRecord(
    int Count,
    Action<EntityComponentAccessor>? Initializer);

internal readonly record struct DestructionCommandRecord(
    IEnumerable<Entity> Entities,
    Action<EntityComponentAccessor>? Finalizer);