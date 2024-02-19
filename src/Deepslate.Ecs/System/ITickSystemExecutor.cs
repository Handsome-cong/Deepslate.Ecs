namespace Deepslate.Ecs;

public interface ITickSystemExecutor : IDisposable
{
    void Execute(EntityCommand command);

    void PostInitialize(World world)
    {
    }

    void IDisposable.Dispose()
    {
    }
}