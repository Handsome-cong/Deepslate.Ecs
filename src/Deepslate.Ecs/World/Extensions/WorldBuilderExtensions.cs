namespace Deepslate.Ecs.Extensions;

public static partial class WorldBuilderExtensions
{
    public static WorldBuilder AddStage(this WorldBuilder builder, Action<StageBuilder> configure)
    {
        return builder.AddStage(configure, out _);
    }

    public static WorldBuilder AddStage(
        this WorldBuilder builder,
        Action<StageBuilder> configure,
        out Stage? configuredStage)
    {
        var stageBuilder = builder.AddStage();
        configure(stageBuilder);
        configuredStage = stageBuilder.Result;
        return builder;
    }

    public static WorldBuilder AddStage(this WorldBuilder builder, Func<StageBuilder, Stage> configure)
    {
        return builder.AddStage(configure, out _);
    }

    public static WorldBuilder AddStage(
        this WorldBuilder builder,
        Func<StageBuilder, Stage> configure,
        out Stage configuredStage)
    {
        var stageBuilder = builder.AddStage();
        configuredStage = configure(stageBuilder);
        return builder;
    }

    public static WorldBuilder WithArchetype(this WorldBuilder builder, Action<ArchetypeBuilder> configure)
    {
        return builder.WithArchetype(configure, out _);
    }

    public static WorldBuilder WithArchetype(
        this WorldBuilder builder,
        Action<ArchetypeBuilder> configure,
        out Archetype? configuredArchetype)
    {
        var archetypeBuilder = builder.WithArchetype();
        configure(archetypeBuilder);
        configuredArchetype = archetypeBuilder.Result;
        return builder;
    }

    public static WorldBuilder WithArchetype(this WorldBuilder builder, Func<ArchetypeBuilder, Archetype> configure)
    {
        return builder.WithArchetype(configure, out _);
    }

    public static WorldBuilder WithArchetype(
        this WorldBuilder builder,
        Func<ArchetypeBuilder, Archetype> configure,
        out Archetype configuredArchetype)
    {
        var archetypeBuilder = builder.WithArchetype();
        configuredArchetype = configure(archetypeBuilder);
        return builder;
    }
}