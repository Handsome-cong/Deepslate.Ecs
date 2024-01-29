namespace Deepslate.Ecs;

public interface ISystemConfig
{
    public static readonly ISystemConfig Default = new DefaultSystemConfig();
    
    string Name { get; }
    bool Enabled { get; }
}

internal class DefaultSystemConfig : ISystemConfig
{
    public string Name => "Default";
    public bool Enabled => true;
}
