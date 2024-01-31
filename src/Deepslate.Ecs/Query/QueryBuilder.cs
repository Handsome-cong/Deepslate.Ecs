using Deepslate.Ecs.GenericWrapper;

namespace Deepslate.Ecs;

public partial class QueryBuilder(World world)
{
    private World World { get; } = world;

    private List<Type> RequiredWritableComponentTypes { get; } = [];
    private List<Type> RequiredReadOnlyComponentTypes { get; } = [];
    private List<Type> IncludedComponentTypes { get; } = [];
    private List<Type> ExcludedComponentTypes { get; } = [];
    private List<Func<Archetype, bool>> Filters { get; } = [];
    private ArchetypeCommandType ArchetypeCommandType { get; set; } = ArchetypeCommandType.None;

    public QueryBuilder(QueryBuilder other)
        : this(other.World)
    {
        RequiredWritableComponentTypes.AddRange(other.RequiredWritableComponentTypes);
        RequiredReadOnlyComponentTypes.AddRange(other.RequiredReadOnlyComponentTypes);
        IncludedComponentTypes.AddRange(other.IncludedComponentTypes);
        ExcludedComponentTypes.AddRange(other.ExcludedComponentTypes);
        Filters.AddRange(other.Filters);
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

    public QueryBuilder WithFilter(Func<Archetype, bool> predicate)
    {
        Filters.Add(predicate);
        return this;
    }

    public QueryBuilder RequireArchetypeCommand(ArchetypeCommandType requirement)
    {
        ArchetypeCommandType = requirement;
        return this;
    }
}

/// <summary>
/// This class exists for keeping <see cref="QueryBuilder"/> clean. <br/>
/// </summary>
public static class QueryBuilderExtensions
{
    public static Writable<TWritable>.ReadOnly.QueryBuilder RequireWritable<TWritable>(this QueryBuilder self)
        where TWritable : IComponent => new(self.RequireWritable(typeof(TWritable)));
    
    public static Writable<TWritable>.ReadOnly.QueryBuilder RequireReadOnly<TWritable>(this QueryBuilder self)
        where TWritable : IComponent => new(self.RequireReadOnly(typeof(TWritable)));
    
    public static Writable.ReadOnly.QueryBuilder With<TWritable>(this QueryBuilder self)
        where TWritable : IComponent => new(self.With(typeof(TWritable)));
    
    public static Writable.ReadOnly.QueryBuilder Without<TWritable>(this QueryBuilder self)
        where TWritable : IComponent => new(self.Without(typeof(TWritable)));
    
    public static Writable.ReadOnly.QueryBuilder WithFilter(this QueryBuilder self, Func<Archetype, bool> predicate)
        => new(self.WithFilter(predicate));
    
    
}
