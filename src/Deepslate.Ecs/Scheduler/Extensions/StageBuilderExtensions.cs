namespace Deepslate.Ecs.Extensions;

public static class StageBuilderExtensions
{
    public static StageBuilder AddTickSystem(this StageBuilder builder, Action<TickSystemBuilder> configure)
    {
        return builder.AddTickSystem(configure, out _);
    }

    public static StageBuilder AddTickSystem(
        this StageBuilder builder,
        Action<TickSystemBuilder> configure,
        out TickSystem? configuredTickSystem)
    {
        var tickSystemBuilder = builder.AddTickSystem();
        configure(tickSystemBuilder);
        configuredTickSystem = tickSystemBuilder.Result;
        return builder;
    }

    public static StageBuilder AddTickSystem(this StageBuilder builder, Func<TickSystemBuilder, TickSystem> configure)
    {
        return builder.AddTickSystem(configure, out _);
    }

    public static StageBuilder AddTickSystem(
        this StageBuilder builder,
        Func<TickSystemBuilder, TickSystem> configure,
        out TickSystem configuredTickSystem)
    {
        var tickSystemBuilder = builder.AddTickSystem();
        configuredTickSystem = configure(tickSystemBuilder);
        return builder;
    }

    public static WorldBuilder Build(this StageBuilder builder, out Stage configuredStage)
    {
        return builder.Build(builder.WorldBuilder.Stages.Count, out configuredStage);
    }
    
    public static WorldBuilder Build(this StageBuilder builder, int index)
    {
        return builder.Build(index, out _);
    }
    
    public static WorldBuilder Build(this StageBuilder builder)
    {
        return builder.Build(builder.WorldBuilder.Stages.Count, out _);
    }
}