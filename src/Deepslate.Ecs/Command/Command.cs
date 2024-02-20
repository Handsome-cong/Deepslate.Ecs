using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

public readonly ref struct Command
{
    private readonly CommandBuffer _commandBufferEndOfStage;
    private readonly CommandBuffer _commandBufferEndOfTick;

    private readonly IReadOnlyList<Archetype> _archetypes;

    public TickSystem TickSystem { get; }


    internal Command(
        TickSystem tickSystem,
        CommandBuffer commandBufferEndOfStage,
        CommandBuffer commandBufferEndOfTick)
    {
        TickSystem = tickSystem;
        _commandBufferEndOfStage = commandBufferEndOfStage;
        _commandBufferEndOfTick = commandBufferEndOfTick;
        _archetypes = tickSystem.Stage.Scheduler.World.Archetypes;
    }

    /// <summary>
    /// <para>
    /// Record a create operation in the buffer.
    /// The <paramref name="archetype"/> must be matched by any query that requires instant command.
    /// </para>
    /// <para>
    /// This method is thread-safe, but may cause undefined behavior when the buffer is being executed.
    /// </para>
    /// </summary>
    /// <param name="buffer">
    /// The buffer to record the command.
    /// </param>
    /// <param name="archetype"></param>
    /// <param name="initializer">
    /// An optional initializer that will be invoked after the entity is created
    /// and <see cref="IReactAfterCreate{TComponent}"/> is invoked.
    /// </param>
    /// <param name="count">
    /// The number of entities to create.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="archetype"/> is null or <see cref="Archetype.Empty"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the archetype is not matched by any query that requires instant command.
    /// </exception>
    /// <seealso cref="TryRecordCreate"/>
    /// <seealso cref="Create"/>
    /// <seealso cref="TryCreate"/>
    /// <seealso cref="DeferCreate"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RecordCreate(
        CommandBuffer buffer,
        Archetype archetype,
        Action<EntityComponentAccessor>? initializer = null,
        int count = 1)
    {
        ThrowIfArchetypeWithoutInstantCommand(archetype);
        buffer.AddCreationCommand(archetype, new CreationCommandRecord(count, initializer));
    }

    /// <summary>
    /// <para>
    /// Try to record a create operation in the buffer.
    /// May fail if the <paramref name="archetype"/> is not matched by any query that requires instant command.
    /// </para>
    /// <para>
    /// This method is thread-safe, but may cause undefined behavior when the buffer is being executed.
    /// </para>
    /// </summary>
    /// <param name="buffer">
    /// The buffer to record the command.
    /// </param>
    /// <param name="archetype"></param>
    /// <param name="initializer">
    /// An optional initializer that will be invoked after the entity is created
    /// and <see cref="IReactAfterCreate{TComponent}"/> is invoked.
    /// </param>
    /// <param name="count">
    /// The number of entities to create.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the operation is recorded successfully, otherwise <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="archetype"/> is null or <see cref="Archetype.Empty"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the archetype is not matched by any query that requires instant command.
    /// </exception>
    /// <seealso cref="RecordCreate"/>
    /// <seealso cref="Create"/>
    /// <seealso cref="TryDestroy"/>
    /// <seealso cref="DeferCreate"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>   
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryRecordCreate(
        CommandBuffer buffer,
        Archetype archetype,
        Action<EntityComponentAccessor>? initializer = null,
        int count = 1)
    {
        if (TickSystem.IsWithInstantCommand(archetype))
        {
            return false;
        }

        buffer.AddCreationCommand(archetype, new CreationCommandRecord(count, initializer));
        return true;
    }

    /// <summary>
    /// <para>
    /// Record a destroy operation in the buffer.
    /// The <paramref name="entities"/> must exist in archetypes that are matched by
    /// any query that requires instant command.
    /// </para>
    /// <para>
    /// This method is thread-safe, but may cause undefined behavior when the buffer is being executed.
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
    /// <exception cref="InvalidOperationException">
    /// Thrown if any archetype of the entities is not matched by any query that requires instant command.
    /// </exception>
    /// <seealso cref="TryRecordDestroy"/>
    /// <seealso cref="Destroy"/>
    /// <seealso cref="TryDestroy"/>
    /// <seealso cref="DeferDestroy"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int RecordDestroy(
        CommandBuffer buffer,
        IEnumerable<Entity> entities,
        Action<EntityComponentAccessor>? finalizer = null)
    {
        var count = 0;
        var entityDictionary = new Dictionary<Archetype, HashSet<Entity>>();
        foreach (var entity in entities)
        {
            if (!TryGetArchetypeIfMatched(entity, out var archetype))
            {
                throw new InvalidOperationException("The archetype of the entity is not matched by any query.");
            }

            if (!TickSystem.IsWithInstantCommand(archetype))
            {
                throw new InvalidOperationException(
                    "The archetype is not matched by any query that requires instant command.");
            }

            ref var set = ref CollectionsMarshal.GetValueRefOrAddDefault(entityDictionary, archetype, out _);
            set ??= [];
            set.Add(entity);
            if (set.Add(entity))
            {
                count++;
            }
        }

        foreach (var (archetype, entityList) in entityDictionary)
        {
            buffer.AddDestructionCommand(archetype, new DestructionCommandRecord(entityList, finalizer));
        }

        return count;
    }

    /// <summary>
    /// <para>
    /// Try to record a destroy operation in the buffer.
    /// The entities in archetypes which are not matched by any query that requires instant command will be ignored.
    /// </para>
    /// <para>
    /// This method is thread-safe, but may cause undefined behavior when the buffer is being executed.
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
    /// </param>
    /// <returns>
    /// The number of entities that recorded successfully.
    /// </returns>
    /// <seealso cref="RecordDestroy"/>
    /// <seealso cref="Destroy"/>
    /// <seealso cref="TryDestroy"/>
    /// <seealso cref="DeferDestroy"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int TryRecordDestroy(
        CommandBuffer buffer,
        IEnumerable<Entity> entities,
        Action<EntityComponentAccessor>? finalizer = null)
    {
        var count = 0;
        var entityDictionary = new Dictionary<Archetype, HashSet<Entity>>();
        foreach (var entity in entities)
        {
            if (!TryGetArchetypeIfMatched(entity, out var archetype))
            {
                continue;
            }

            if (!TickSystem.IsWithInstantCommand(archetype))
            {
                continue;
            }

            ref var set = ref CollectionsMarshal.GetValueRefOrAddDefault(entityDictionary, archetype, out _);
            set ??= [];
            if (set.Add(entity))
            {
                count++;
            }
        }

        foreach (var (archetype, entityList) in entityDictionary)
        {
            buffer.AddDestructionCommand(archetype, new DestructionCommandRecord(entityList, finalizer));
        }

        return count;
    }

    /// <summary>
    /// <para>
    /// Execute all commands in the buffer.
    /// Destruction commands will be executed first, then creation commands.
    /// </para>
    /// <para>
    /// Recording commands to a buffer that is being executed will cause undefined behavior.
    /// </para>
    /// </summary>
    /// <seealso cref="ExecuteCommandBufferInParallelAsync"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ExecuteCommandBuffer(CommandBuffer buffer)
    {
        buffer.Execute();
    }


    /// <summary>
    /// <para>
    /// Execute all commands in the buffer in parallel.
    /// Destruction commands will be executed first, then creation commands.
    /// Commands with different archetypes will be executed in parallel.
    /// </para>
    /// <para>
    /// The tick system must be ensured to be completed after the returned task is completed.
    /// </para>
    /// <para>
    /// Recording commands to a buffer that is being executed will cause undefined behavior.
    /// </para>
    /// </summary>
    /// <seealso cref="ExecuteCommandBuffer"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task ExecuteCommandBufferInParallelAsync(CommandBuffer buffer)
    {
        return buffer.ParallelExecuteAsync();
    }

    /// <summary>
    /// Create an <see cref="Entity"/> in this <see cref="Archetype"/>.
    /// The <paramref name="archetype"/> must be matched by any query that requires instant command.
    /// </summary>
    /// <param name="archetype">
    /// The archetype of the entity to create.
    /// </param>
    /// <returns>
    /// The created <see cref="Entity"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="archetype"/> is null or <see cref="Archetype.Empty"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the archetype is not matched by any query that requires instant command.
    /// </exception>
    /// <seealso cref="TryCreate"/>
    /// <seealso cref="DeferCreate"/>
    /// <seealso cref="RecordCreate"/>
    /// <seealso cref="TryRecordCreate"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create(Archetype archetype)
    {
        ThrowIfArchetypeWithoutInstantCommand(archetype);
        return archetype.Create();
    }

    /// <summary>
    /// Try to create an <see cref="Entity"/> in this <see cref="Archetype"/>.
    /// May fail if the <paramref name="archetype"/> is not matched by any query that requires instant command.
    /// </summary>
    /// <param name="archetype">
    /// The archetype of the entity to create.
    /// </param>
    /// <param name="entity">
    /// The created <see cref="Entity"/>.
    /// May be <see cref="Entity.Dead"/> if the creation fails.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the entity is created successfully, otherwise <see langword="false"/>.
    /// </returns>
    /// <seealso cref="Create"/>
    /// <seealso cref="DeferCreate"/>
    /// <seealso cref="RecordCreate"/>
    /// <seealso cref="TryRecordCreate"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryCreate(Archetype archetype, out Entity entity)
    {
        if (IsArchetypeMatchedWithInstantCommand(archetype))
        {
            entity = archetype.Create();
            return true;
        }

        entity = Entity.Dead;
        return false;
    }

    /// <summary>
    /// Destroy the <see cref="Entity"/> from this <see cref="Archetype"/>.
    /// The <paramref name="entity"/> must exist in an archetype that is matched
    /// by any query that requires instant command.
    /// </summary>
    /// <param name="entity">
    /// The entity you want to destroy.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the entity does not exist in any matched archetype with instant command.
    /// </exception>
    /// <seealso cref="TryDestroy"/>
    /// <seealso cref="DeferDestroy"/>
    /// <seealso cref="RecordDestroy"/>
    /// <seealso cref="TryRecordDestroy"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(Entity entity)
    {
        ThrowIfEntityWithoutInstantCommand(entity);
        _archetypes[entity.ArchetypeId].Destroy(entity);
    }

    /// <summary>
    /// Destroy the <see cref="Entity"/> from this <see cref="Archetype"/>.
    /// May fail if the <paramref name="entity"/> does not exist in an archetype that is matched
    /// by any query that requires instant command.
    /// </summary>
    /// <param name="entity">
    /// The entity you want to destroy.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="entity"/> destroyed successfully.
    /// May return <see langword="false"/> if the <paramref name="entity"/> does not exist.
    /// </returns>
    /// <seealso cref="Destroy"/>
    /// <seealso cref="DeferDestroy"/>
    /// <seealso cref="RecordDestroy"/>
    /// <seealso cref="TryRecordDestroy"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryDestroy(Entity entity)
    {
        if (!TryGetArchetype(entity, out var archetype))
        {
            return false;
        }

        if (!IsArchetypeMatchedWithInstantCommand(archetype))
        {
            return false;
        }

        return archetype.Destroy(entity);
    }

    /// <summary>
    /// Try to get the <see cref="Archetype"/> of the entity, if the archetype is matched
    /// by any query of the <see cref="TickSystem"/>.
    /// </summary>
    /// <param name="entity">
    /// The entity whose archetype you want to get.
    /// </param>
    /// <param name="archetype">
    /// The archetype of the entity.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the archetype is found, otherwise <see langword="false"/>.
    /// </returns>
    /// <seealso cref="TryGetArchetype"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetypeIfMatched(Entity entity, [MaybeNullWhen(false)] out Archetype archetype)
    {
        if (TickSystem.MatchedArchetypesById.TryGetValue(entity.ArchetypeId, out archetype))
        {
            if (archetype.ContainsEntity(entity))
            {
                return true;
            }
        }

        archetype = null;
        return false;
    }

    /// <summary>
    /// Try to get the <see cref="Archetype"/> of the entity.
    /// This method will search the <see cref="Archetype"/> globally, so the <paramref name="archetype"/> output
    /// may not be matched by any query of the <see cref="TickSystem"/>.
    /// </summary>
    /// <param name="entity">
    /// The entity whose archetype you want to get.
    /// </param>
    /// <param name="archetype">
    /// The archetype of the entity.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the archetype is found, otherwise <see langword="false"/>.
    /// </returns>
    /// <seealso cref="TryGetArchetypeIfMatched"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(Entity entity, [MaybeNullWhen(false)] out Archetype archetype)
    {
        if (TryGetArchetypeById(entity.ArchetypeId, out archetype))
        {
            if (archetype.ContainsEntity(entity))
            {
                return true;
            }
        }

        archetype = null;
        return false;
    }

    private bool TryGetArchetypeById(ushort id, [MaybeNullWhen(false)] out Archetype archetype)
    {
        if (id >= _archetypes.Count)
        {
            archetype = null;
            return false;
        }

        archetype = _archetypes[id];
        return true;
    }

    /// <summary>
    /// Get the writable component of the entity.
    /// </summary>
    /// <param name="entity">
    /// The entity whose component you want to get.
    /// </param>
    /// <typeparam name="TComponent">
    /// The type of the component you want to get.
    /// </typeparam>
    /// <returns>
    /// The component reference you want to get.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the entity does not exist in any matched archetype.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the component is not writable or does not exist.
    /// </exception>
    /// <seealso cref="GetReadOnlyComponent{TComponent}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref TComponent GetWritableComponent<TComponent>(Entity entity)
        where TComponent : IComponent
    {
        if (!TryGetArchetypeIfMatched(entity, out var archetype))
        {
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity does not exist in any matched archetype.");
        }

        if (!TickSystem.IsWritable<TComponent>(archetype))
        {
            throw new InvalidOperationException("The component is not writable or does not exist.");
        }

        return ref archetype.GetComponent<TComponent>(entity);
    }

    /// <summary>
    /// Get the read-only component of the entity.
    /// </summary>
    /// <param name="entity">
    /// The entity whose component you want to get.
    /// </param>
    /// <typeparam name="TComponent">
    /// The type of the component you want to get.
    /// </typeparam>
    /// <returns>
    /// The component reference you want to get.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the entity does not exist in any matched archetype.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the component is not readable or does not exist.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref readonly TComponent GetReadOnlyComponent<TComponent>(Entity entity)
        where TComponent : IComponent
    {
        if (!TryGetArchetypeIfMatched(entity, out var archetype))
        {
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity does not exist in any matched archetype.");
        }

        if (!TickSystem.IsReadable<TComponent>(archetype))
        {
            throw new InvalidOperationException("The component is not writable or does not exist.");
        }

        return ref archetype.GetComponent<TComponent>(entity);
    }

    [Conditional("DEBUG")]
    private void ThrowIfEntityWithoutInstantCommand(Entity entity)
    {
        var archetypeId = entity.ArchetypeId;
        if (archetypeId >= _archetypes.Count)
        {
            throw new InvalidOperationException("The archetype of the entity does not exist.");
        }

        var archetype = _archetypes[archetypeId];
        ThrowIfArchetypeWithoutInstantCommand(archetype);
        if (!archetype.ContainsEntity(entity))
        {
            throw new InvalidOperationException("The entity does not exist in the archetype.");
        }
    }

    [Conditional("DEBUG")]
    private void ThrowIfArchetypeWithoutInstantCommand(Archetype archetype)
    {
        Guard.ArchetypeIsNotEmptyOrNull(archetype);
        if (!IsArchetypeMatchedWithInstantCommand(archetype))
        {
            throw new InvalidOperationException(
                "The archetype is not matched by any query that requires instant command.");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsArchetypeMatchedWithInstantCommand(Archetype archetype)
    {
        return TickSystem.MatchedArchetypesWithInstantCommand.Contains(archetype);
    }

    /// <summary>
    /// Enqueue a create operation which will be delayed.
    /// </summary>
    /// <param name="archetype">
    /// The archetype of the entity to create.
    /// </param>
    /// <param name="initializer">
    /// A handler that will be invoked after the entity is created.
    /// </param>
    /// <param name="count">
    /// The number of entities to create.
    /// </param>
    /// <param name="timePoint">
    /// Indicates when the command will be executed.
    /// </param>
    /// <seealso cref="Create"/>
    /// <seealso cref="TryCreate"/>
    /// <seealso cref="RecordCreate"/>
    /// <seealso cref="TryRecordCreate"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeferCreate(
        Archetype archetype,
        Action<EntityComponentAccessor>? initializer = null,
        int count = 1,
        ExecuteTimePoint timePoint = ExecuteTimePoint.EndOfStage)
    {
        switch (timePoint)
        {
            case ExecuteTimePoint.EndOfStage:
                _commandBufferEndOfStage.AddCreationCommand(archetype, new CreationCommandRecord(count, initializer));
                break;
            case ExecuteTimePoint.EndOfTick:
                _commandBufferEndOfTick.AddCreationCommand(archetype, new CreationCommandRecord(count, initializer));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(timePoint), timePoint, "Invalid time point.");
        }
    }

    /// <summary>
    /// Enqueue a destroy operation which will be delayed.
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
    /// <seealso cref="Destroy"/>
    /// <seealso cref="TryDestroy"/>
    /// <seealso cref="RecordDestroy"/>
    /// <seealso cref="TryRecordDestroy"/>
    /// <seealso cref="QueryBuilder.RequireInstantCommand"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int DeferDestroy(
        IEnumerable<Entity> entities,
        Action<EntityComponentAccessor>? finalizer = null,
        ExecuteTimePoint timePoint = ExecuteTimePoint.EndOfStage)
    {
        var count = 0;
        var commandBuffer = timePoint switch
        {
            ExecuteTimePoint.EndOfStage => _commandBufferEndOfStage,
            ExecuteTimePoint.EndOfTick => _commandBufferEndOfTick,
            _ => throw new ArgumentOutOfRangeException(nameof(timePoint), timePoint, "Invalid time point.")
        };
        var groupedEntities = entities.GroupBy(entity => entity.ArchetypeId);
        foreach (var group in groupedEntities)
        {
            if (TryGetArchetypeById(group.Key, out var archetype))
            {
                commandBuffer.AddDestructionCommand(archetype,
                    new DestructionCommandRecord(group.ToArray(), finalizer));
                count++;
            }
        }

        return count;
    }

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

    /// <summary>
    /// <para>
    /// Get the resource of the specified type.
    /// </para>
    /// <para>
    /// Make sure the resource is required by <see cref="TickSystemBuilder.WithResource{TResource}"/>
    /// of <see cref="TickSystemBuilder"/> and the resource factory is valid.
    /// </para>
    /// </summary>
    /// <typeparam name="TResource">
    /// The type of the resource.
    /// </typeparam>
    /// <returns>
    /// The resource you want to get.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the resource is not registered, not required, or the resource factory is invalid.
    /// </exception>
    /// <seealso cref="TryGetResource{TResource}"/>
    public TResource GetResource<TResource>()
        where TResource : IResource
    {
        if (!TickSystem.ResourceFactories.TryGetValue(typeof(TResource), out var factory))
        {
            throw new InvalidOperationException("The resource is not required by the tick system.");
        }

        if (factory is not Func<TResource> resourceFactory)
        {
            throw new InvalidOperationException("The resource factory registered is invalid.");
        }

        return resourceFactory();
    }

    /// <summary>
    /// <para>
    /// Try to get the resource of the specified type.
    /// </para>
    /// <para>
    /// May fail if the resource is not required by <see cref="TickSystemBuilder.WithResource{TResource}"/>
    /// of <see cref="TickSystemBuilder"/> or the resource factory is invalid.
    /// </para>
    /// </summary>
    /// <param name="resource">
    /// The resource you want to get.
    /// </param>
    /// <typeparam name="TResource">
    /// The type of the resource.
    /// </typeparam>
    /// <returns>
    /// <see langword="true"/> if the resource is got successfully, otherwise <see langword="false"/>.
    /// </returns>
    /// <seealso cref="GetResource{TResource}"/>
    public bool TryGetResource<TResource>([MaybeNullWhen(false)] out TResource resource)
        where TResource : IResource
    {
        resource = default;

        if (!TickSystem.ResourceFactories.TryGetValue(typeof(TResource), out var factory))
        {
            return false;
        }

        if (factory is not Func<TResource> resourceFactory)
        {
            return false;
        }

        resource = resourceFactory();
        return true;
    }
}