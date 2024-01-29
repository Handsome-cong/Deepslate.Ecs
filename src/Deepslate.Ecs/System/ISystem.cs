using Deepslate.Ecs;

namespace Deepslate.Ecs;

public interface ISystem
{
    ISystemConfig Config => ISystemConfig.Default;
    
    void Initialize(World world);
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
