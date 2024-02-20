namespace Deepslate.Ecs;

public interface ITickSystemExecutor : IDisposable
{
    void Execute(Command command);

    void PostInitialize(World world)
    {
    }

    void IDisposable.Dispose()
    {
    }
}