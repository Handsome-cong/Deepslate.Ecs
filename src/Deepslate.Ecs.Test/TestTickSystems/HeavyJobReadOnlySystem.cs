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
            .WithReadOnly<TComponent>()
            .Build(out _);
    }

    public void Execute(Command command)
    {
        var sw = new Stopwatch();
        sw.Start();
        Thread.Sleep(TimeSpan.FromMilliseconds(HeavyJobSystem.ExecutionElapsedTime));
        sw.Stop();
        ElapsedTime = sw.ElapsedMilliseconds;
    }
}