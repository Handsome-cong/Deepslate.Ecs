namespace Deepslate.Ecs;

public interface ITickSystemExecutor : IDisposable
{
    void Execute();

    void IDisposable.Dispose()
    {
    }
}