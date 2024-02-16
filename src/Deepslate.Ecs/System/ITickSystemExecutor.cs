namespace Deepslate.Ecs;

public interface ITickSystemExecutor : IDisposable
{
    void Execute(TickSystemCommand command);

    void IDisposable.Dispose()
    {
    }
}