namespace Deepslate.Ecs.Extensions;

public static class TickSystemExtensions
{
    public static bool IsWritable<TComponent>(this TickSystem system, Archetype archetype)
        where TComponent : IComponentData
    {
        return system.IsWritable(archetype, typeof(TComponent));
    }
    
    public static bool IsWritable(this TickSystem system, Archetype archetype, Type componentType)
    {
        system.WritableComponentTypes.TryGetValue(archetype, out var types);
        return types?.Contains(componentType) ?? false;
    }
    
    public static bool IsReadable<TComponent>(this TickSystem system, Archetype archetype)
        where TComponent : IComponentData
    {
        return system.IsReadable(archetype, typeof(TComponent));
    }
    
    public static bool IsReadable(this TickSystem system, Archetype archetype, Type componentType)
    {
        system.ReadableComponentTypes.TryGetValue(archetype, out var types);
        return types?.Contains(componentType) ?? false;
    }
    
    public static bool IsWithInstantCommand(this TickSystem system, Archetype archetype)
    {
        return system.MatchedArchetypesWithInstantCommand.Contains(archetype);
    }
    
    public static bool IsMatched(this TickSystem system, Archetype archetype)
    {
        return system.MatchedArchetypes.Contains(archetype);
    }
}