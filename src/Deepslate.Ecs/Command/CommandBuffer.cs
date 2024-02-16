namespace Deepslate.Ecs;

/// <summary>
/// A reusable buffer that stores commands which can be executed later manually.
/// The add operations are thread-safe, but the execute operations are not.
/// </summary>
/// <param name="system">
/// The tick system that the buffer is associated with.
/// </param>
public sealed class CommandBuffer(TickSystem system)
{
    private readonly UncheckedCommandBuffer _buffer = new();

    internal void AddCreationCommand(Archetype archetype, CreationCommandRecord record)
    {
        _buffer.AddCreationCommand(archetype, record);
    }

    internal void AddDestructionCommand(Archetype archetype, DestructionCommandRecord record)
    {
        _buffer.AddDestructionCommand(archetype, record);
    }

    /// <summary>
    /// Clear all commands in the buffer.
    /// </summary>
    public void Clear()
    {
        _buffer.Clear();
    }

    /// <summary>
    /// Execute all commands in the buffer.
    /// Destruction commands will be executed first, then creation commands.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the tick system is not being executed.
    /// </exception>
    /// <seealso cref="ParallelExecuteAsync"/>
    public void Execute()
    {
        ThrowIfTickSystemIsNotExecuting();
        _buffer.Execute();
    }

    /// <summary>
    /// <para>
    /// Execute all commands in the buffer in parallel.
    /// Destruction commands will be executed first, then creation commands.
    /// Commands with different archetypes will be executed in parallel.
    /// </para>
    /// <para>
    /// The tick system must be ensured to be completed after this method is completed.
    /// </para>
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the tick system is not running.
    /// </exception>
    /// <seealso cref="Execute"/>
    public Task ParallelExecuteAsync()
    {
        ThrowIfTickSystemIsNotExecuting();
        return _buffer.ParallelExecuteAsync();
    }

    private void ThrowIfTickSystemIsNotExecuting()
    {
        if (!system.Running)
        {
            throw new InvalidOperationException("The tick system is not running.");
        }
    }
}