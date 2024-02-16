namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class EmptySystem : ITickSystemExecutor
{
    public void Execute(TickSystemCommand command)
    {
        throw new NotImplementedException();
    }
}