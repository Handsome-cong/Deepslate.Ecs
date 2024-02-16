using Deepslate.Ecs.GenericWrapper;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

public partial class QueryBuilder
{
    private List<Type> RequiredWritableComponentTypes { get; } = [];
    private List<Type> RequiredReadOnlyComponentTypes { get; } = [];
    private List<Type> IncludedComponentTypes { get; } = [];
    private List<Type> ExcludedComponentTypes { get; } = [];

    private Predicate<Archetype>? _filter; 
    private bool _requireInstantArchetypeCommand = false;

    public Query? Result { get; private set; }
    public TickSystemBuilder TickSystemBuilder { get; }

    internal QueryBuilder(TickSystemBuilder tickSystemBuilder)
    {
        TickSystemBuilder = tickSystemBuilder;
    }

    public QueryBuilder RequireWritable(Type type)
    {
        With(type);
        RequiredWritableComponentTypes.Add(type);
        return this;
    }

    public QueryBuilder RequireReadOnly(Type type)
    {
        With(type);
        RequiredReadOnlyComponentTypes.Add(type);
        return this;
    }

    public QueryBuilder With(Type type)
    {
        Guard.IsComponent(type);
        IncludedComponentTypes.Add(type);
        return this;
    }

    public QueryBuilder Without(Type type)
    {
        Guard.IsComponent(type);
        ExcludedComponentTypes.Add(type);
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

    public static implicit operator Writable.ReadOnly.QueryBuilder(QueryBuilder self) => new(self);
}

/// <summary>
/// This class exists for keeping <see cref="QueryBuilder"/> clean. <br/>
/// </summary>
public static class QueryBuilderExtensions
{
    public static QueryBuilder RequireWritable<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.RequireWritable(typeof(TWritable));

    public static QueryBuilder RequireReadOnly<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.RequireReadOnly(typeof(TWritable));

    public static QueryBuilder With<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.With(typeof(TWritable));

    public static QueryBuilder Without<TWritable>(this QueryBuilder self)
        where TWritable : IComponentData => self.Without(typeof(TWritable));

    public static QueryBuilder WithFilter(this QueryBuilder self, Func<Archetype, bool> predicate)
        => self.WithFilter(predicate);
}