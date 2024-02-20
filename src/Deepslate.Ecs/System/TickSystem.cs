using System.Collections.Frozen;
using System.Runtime.InteropServices;

namespace Deepslate.Ecs;

public sealed class TickSystem
{
    private readonly HashSet<TickSystem> _otherSystemsThisDependsOn;
    private UsageCode[] _usageCodes = Array.Empty<UsageCode>();

    internal Task ExecutionTask = Task.CompletedTask;

    internal IReadOnlySet<TickSystem> OtherSystemsThisDependsOn => _otherSystemsThisDependsOn;
    internal ReadOnlySpan<UsageCode> UsageCodes => _usageCodes;
    internal bool[] InstantCommandFlags { get; private set; } = Array.Empty<bool>();

    internal FrozenDictionary<ushort, Archetype> MatchedArchetypesById { get; private set; } =
        FrozenDictionary<ushort, Archetype>.Empty;
    
    internal FrozenDictionary<Type, Delegate> ResourceFactories { get; private set; } =
        FrozenDictionary<Type, Delegate>.Empty;

    public ITickSystemExecutor Executor { get; }
    public bool Running => !ExecutionTask.IsCompleted;
    public FrozenSet<Query> Queries { get; }

    /// <summary>
    /// All archetypes that are matched by any query in <see cref="Queries"/> of this system. <br/>
    /// Empty until <see cref="WorldBuilder.Build"/> is called.
    /// </summary>
    public FrozenSet<Archetype> MatchedArchetypes { get; private set; } = FrozenSet<Archetype>.Empty;

    /// <summary>
    /// All archetypes that are matched by any query in <see cref="Queries"/> of this system and
    /// require instant command. <br/>
    /// Empty until <see cref="WorldBuilder.Build"/> is called.
    /// </summary>
    public FrozenSet<Archetype> MatchedArchetypesWithInstantCommand { get; private set; } =
        FrozenSet<Archetype>.Empty;

    public FrozenDictionary<Archetype, FrozenSet<Type>> WritableComponentTypes { get; }
    public FrozenDictionary<Archetype, FrozenSet<Type>> ReadableComponentTypes { get; }
    public FrozenSet<Type> ResourceTypes { get; }

    /// <summary>
    /// The stage this system is in. <br/>
    /// Null until <see cref="WorldBuilder.Build"/> is called.
    /// </summary>
    public Stage Stage { get; private set; } = default!;

    internal TickSystem(
        ITickSystemExecutor executor,
        IEnumerable<Query> queries,
        IEnumerable<TickSystem> dependentSystems,
        IEnumerable<Type> resourceTypes)
    {
        Executor = executor;
        Queries = queries.ToFrozenSet();
        _otherSystemsThisDependsOn = [..dependentSystems];
        ResourceTypes = resourceTypes.ToFrozenSet();

        var writableComponentTypes = new Dictionary<Archetype, HashSet<Type>>();
        var readableComponentTypes = new Dictionary<Archetype, HashSet<Type>>();

        foreach (var query in Queries)
        {
            if (query.RequireInstantCommand)
            {
                foreach (var archetype in query.MatchedArchetypes)
                {
                    FillComponentTypes(writableComponentTypes, archetype, archetype.ComponentTypes);
                    FillComponentTypes(readableComponentTypes, archetype, archetype.ComponentTypes);
                }
            }
            else
            {
                foreach (var archetype in query.MatchedArchetypes)
                {
                    FillComponentTypes(writableComponentTypes, archetype, query.RequiredWritableComponentTypes);
                    FillComponentTypes(readableComponentTypes, archetype, query.RequiredWritableComponentTypes);
                    FillComponentTypes(readableComponentTypes, archetype, query.RequiredReadOnlyComponentTypes);
                }
            }
        }

        WritableComponentTypes = writableComponentTypes.ToFrozenDictionary(
            kvp => kvp.Key, kvp => kvp.Value.ToFrozenSet());
        ReadableComponentTypes = readableComponentTypes.ToFrozenDictionary(
            kvp => kvp.Key, kvp => kvp.Value.ToFrozenSet());
    }

    private void FillComponentTypes(
        Dictionary<Archetype, HashSet<Type>> componentTypes,
        Archetype archetype,
        IEnumerable<Type> types)
    {
        ref var hashSetRef =
            ref CollectionsMarshal.GetValueRefOrAddDefault(componentTypes, archetype, out _);
        hashSetRef ??= [];
        foreach (var componentType in types)
        {
            hashSetRef.Add(componentType);
        }
    }

    internal void PostInitialize(World world, Stage stage)
    {
        ThrowIfResourceNotRegistered(world);
        Stage = stage;
        foreach (var query in Queries)
        {
            query.PostInitialize(world);
        }

        var resourceUsageCodeCount = UsageCodeHelper.GetUsageCodeCount(world.ResourceIds.Count);
        var queryUsageCodeCount = Queries.Aggregate(0, (current, query) => current + query.UsageCodes.Length);
        var length = queryUsageCodeCount + resourceUsageCodeCount;
        _usageCodes = new UsageCode[length];
        var resourceUsageCodes = _usageCodes.AsSpan(0, resourceUsageCodeCount);
        var queryUsageCodes = _usageCodes.AsSpan(resourceUsageCodeCount, queryUsageCodeCount);
        
        UsageCodeHelper.FillUsageCode(resourceUsageCodes, ResourceTypes, world.ResourceIds);

        InstantCommandFlags = new bool[Queries.Count];
        var start = 0;
        var i = 0;
        foreach (var query in Queries)
        {
            query.UsageCodes.CopyTo(queryUsageCodes.Slice(start, query.UsageCodes.Length));
            start += query.UsageCodes.Length;
            InstantCommandFlags[i++] = query.RequireInstantCommand;
        }

        MatchedArchetypes = Queries.SelectMany(query => query.MatchedArchetypes).ToFrozenSet();
        MatchedArchetypesWithInstantCommand = Queries.SelectMany(
                query => query.RequireInstantCommand
                    ? query.MatchedArchetypesSet
                    : Enumerable.Empty<Archetype>())
            .ToFrozenSet();
        MatchedArchetypesById = MatchedArchetypes.ToFrozenDictionary(archetype => archetype.Id);
        
        ResourceFactories = ResourceTypes.ToFrozenDictionary(
            type => type, type => world.ResourceFactories[type]);
        
        Executor.PostInitialize(world);
    }
    
    private void ThrowIfResourceNotRegistered(World world)
    {
        if (ResourceTypes.Any(type => !world.ResourceFactories.ContainsKey(type)))
        {
            throw new InvalidOperationException("Resource not registered in world.");
        }
    }
}