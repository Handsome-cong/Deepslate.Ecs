namespace Deepslate.Ecs;

public readonly ref struct TickSystemCommand
{
    private readonly UncheckedCommandBuffer _commandBufferEndOfStage;
    private readonly UncheckedCommandBuffer _commandBufferEndOfTick;
    
    public TickSystem TickSystem { get; }
    

    internal TickSystemCommand(
        TickSystem tickSystem,
        UncheckedCommandBuffer commandBufferEndOfStage,
        UncheckedCommandBuffer commandBufferEndOfTick)
    {
        TickSystem = tickSystem;
        _commandBufferEndOfStage = commandBufferEndOfStage;
        _commandBufferEndOfTick = commandBufferEndOfTick;
    }

    /// <summary>
    /// Create a <see cref="InstantArchetypeCommand"/> which can be used to do operations instantly.
    /// May fail if:
    /// <list type="bullet">
    /// <item><description>The tick system is not running.</description></item>
    /// <item><description>The query does not require <see cref="InstantArchetypeCommand"/>.</description></item>
    /// <item><description>The query is owned by the <see cref="TickSystem"/>.</description></item>
    /// <item><description>The archetype is not matched by the query.</description></item>
    /// </list>
    /// </summary>
    /// <param name="query">
    /// The query that the archetype is matched by and owned by the <see cref="TickSystem"/>.
    /// </param>
    /// <param name="archetype">
    /// The archetype to be operated on.
    /// </param>
    /// <param name="command">
    /// The created command.
    /// </param>
    /// <returns>
    /// Whether the command is created successfully.
    /// </returns>
    public bool TryCreateInstantArchetypeCommand(Query query, Archetype archetype, out InstantArchetypeCommand? command)
    {
        if (!query.RequireInstantArchetypeCommand ||
            !TickSystem.Queries.Contains(query) ||
            !query.MatchedArchetypesSet.Contains(archetype))
        {
            command = null;
            return false;
        }

        command = new InstantArchetypeCommand(archetype, TickSystem);
        return true;
    }

    /// <summary>
    /// Create a <see cref="DeferredArchetypeCommand"/> which can be used to do operations
    /// at the end of stage or tick.
    /// May fail if the tick system is not running.
    /// </summary>
    /// <param name="archetype">
    /// The archetype to be operated on.
    /// </param>
    /// <param name="command">
    /// The created command.
    /// </param>
    /// <returns>
    /// Whether the command is created successfully.
    /// </returns>
    public bool TryCreateDeferredArchetypeCommand(
        Archetype archetype,
        out DeferredArchetypeCommand? command)
    {
        // Maybe we should add a check for whether the archetype and the tick system are in the same world.
        
        command = new DeferredArchetypeCommand(archetype, _commandBufferEndOfStage, _commandBufferEndOfTick);
        return true;
    }
}