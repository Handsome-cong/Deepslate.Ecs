namespace Deepslate.Ecs.Test.Extensions;

internal static class TickSystemExtensions
{
    public static UsageCodeBundle CreateUsageCodeBundle(this TickSystem tickSystem)
    {
        var world = tickSystem.Stage.Scheduler.World;
        return new UsageCodeBundle(
            tickSystem.UsageCodes, 
            tickSystem.InstantCommandFlags, 
            world.ResourceFactories.Count,
            world.Archetypes.Count, 
            world.ComponentTypes.Count);
    }
}