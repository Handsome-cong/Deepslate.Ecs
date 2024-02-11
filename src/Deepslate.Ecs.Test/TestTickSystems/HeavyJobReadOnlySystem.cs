using System.Diagnostics;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class HeavyJobReadOnlySystem<TComponent> : ITickSystemExecutor, ITimeRecorded
    where TComponent : IComponentData
{
    public long ElapsedTime { get; private set; }

    public HeavyJobReadOnlySystem(TickSystemBuilder builder)
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
        Thread.Sleep(TimeSpan.FromMilliseconds(HeavyJobSystem.ExecutionElapsedTime));
        sw.Stop();
        ElapsedTime = sw.ElapsedMilliseconds;
    }
}