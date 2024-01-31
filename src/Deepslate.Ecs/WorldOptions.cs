namespace Deepslate.Ecs;

public sealed class WorldOptions
{
    public static WorldOptions Default { get; } = new();
    
    public IComponentPoolFactory ComponentPoolFactory { get; set; } = new DefaultComponentPoolFactory();
}