using Deepslate.Ecs.GenericWrapper;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

public class QueryBuilder
{
    private readonly List<Type> _requiredWritableComponentTypes  = [];
    private readonly List<Type> _requiredReadOnlyComponentTypes  = [];
    private readonly List<Type> _includedComponentTypes = [];
    private readonly List<Type> _excludedComponentTypes  = [];

    private Predicate<Archetype>? _filter; 
    private bool _requireInstantArchetypeCommand;

    public Query? Result { get; private set; }
    public TickSystemBuilder TickSystemBuilder { get; }

    internal QueryBuilder(TickSystemBuilder tickSystemBuilder)
    {
        TickSystemBuilder = tickSystemBuilder;
    }

    public QueryBuilder WithWritable(Type type)
    {
        WithIncluded(type);
        _requiredWritableComponentTypes.Add(type);
        return this;
    }

    public QueryBuilder WithReadOnly(Type type)
    {
        WithIncluded(type);
        _requiredReadOnlyComponentTypes.Add(type);
        return this;
    }

    public QueryBuilder WithIncluded(Type type)
    {
        Guard.IsComponent(type);
        _includedComponentTypes.Add(type);
        return this;
    }

    public QueryBuilder WithExcluded(Type type)
    {
        Guard.IsComponent(type);
        _excludedComponentTypes.Add(type);
        return this;
    }

    public QueryBuilder WithFilter(Predicate<Archetype> predicate)
    {
        _filter = predicate;
        return this;
    }

    public QueryBuilder RequireInstantArchetypeCommand()
    {
        _requireInstantArchetypeCommand = true;
        return this;
    }

    public Writable.ReadOnly.QueryBuilder AsGeneric() => new(this);

    
    /// <summary>
    /// Call this to complete the query configuration and register it to the system.
    /// As long as this method is called, the operations on this builder will not affect the query anymore.
    /// </summary>
    /// <param name="configuredQuery">
    /// The query that has been configured and registered.
    /// </param>
    /// <seealso cref="Result"/>
    public TickSystemBuilder Build(out Query configuredQuery)
    {
        if (Result is not null)
        {
            configuredQuery = Result;
        }

        var query = new Query(
            _requiredWritableComponentTypes,
            _requiredReadOnlyComponentTypes,
            _includedComponentTypes,
            _excludedComponentTypes,
            _filter,
            _requireInstantArchetypeCommand);

        Result = query;
        configuredQuery = query;
        TickSystemBuilder.RegisterQuery(query);
        return TickSystemBuilder;
    }
}

/// <summary>
/// This class exists for keeping <see cref="QueryBuilder"/> clean. <br/>
/// </summary>
public static class QueryBuilderExtensions
{
    public static QueryBuilder WithWritable<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.WithWritable(typeof(TWritable));

    public static QueryBuilder WithReadOnly<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.WithReadOnly(typeof(TWritable));

    public static QueryBuilder WithIncluded<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.WithIncluded(typeof(TWritable));

    public static QueryBuilder WithExcluded<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.WithExcluded(typeof(TWritable));
}