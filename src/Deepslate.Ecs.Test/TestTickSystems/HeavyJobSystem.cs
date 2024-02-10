using System.Diagnostics;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class HeavyJobSystem<TComponent> : ITickSystemExecutor
    where TComponent : IComponentData
{
    public const long ExecutionElapsedTime = 1000;
    
    public long ElapsedTime { get; private set; }

    public HeavyJobSystem(TickSystemBuilder builder)
    {
        builder.AddQuery()
            .AsGeneric()
            .RequireReadOnly<TComponent>()
            .Build(out _);
    }
    
    public void Execute()
    {
        var sw = new Stopwatch();
        sw.Start();
        Thread.Sleep(TimeSpan.FromMilliseconds(ExecutionElapsedTime));
        sw.Stop();
        ElapsedTime = sw.ElapsedMilliseconds;
    }
}