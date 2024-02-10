namespace Deepslate.Ecs.Extensions;

public static class TickSystemBuilderExtensions
{
    public static TickSystemBuilder AddQuery(this TickSystemBuilder builder, Action<QueryBuilder> configure)
    {
        return builder.AddQuery(configure, out _);
    }

    public static TickSystemBuilder AddQuery(
        this TickSystemBuilder builder, 
        Action<QueryBuilder> configure,
        out Query? configuredQuery)
    {
        var queryBuilder = builder.AddQuery();
        configure(queryBuilder);
        configuredQuery = queryBuilder.Result;
        return builder;
    }
    
    public static TickSystemBuilder AddQuery(this TickSystemBuilder builder, Func<QueryBuilder, Query> configure)
    {
        return builder.AddQuery(configure, out _);
    }
    
    public static TickSystemBuilder AddQuery(
        this TickSystemBuilder builder, 
        Func<QueryBuilder, Query> configure,
        out Query configuredQuery)
    {
        var queryBuilder = builder.AddQuery();
        configuredQuery = configure(queryBuilder);
        return builder;
    }

    public static StageBuilder Build(this TickSystemBuilder builder, ITickSystemExecutor executor)
    {
        return builder.Build(executor, out _);
    }
}