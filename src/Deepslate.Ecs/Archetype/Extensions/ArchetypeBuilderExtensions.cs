namespace Deepslate.Ecs.Extensions;

public static class ArchetypeBuilderExtensions
{
    public static WorldBuilder Build(this ArchetypeBuilder builder)
    {
        return builder.Build(out _, out _);
    }

    public static WorldBuilder Build(this ArchetypeBuilder builder, out Archetype configuredArchetype)
    {
        return builder.Build(out configuredArchetype, out _);
    }
}