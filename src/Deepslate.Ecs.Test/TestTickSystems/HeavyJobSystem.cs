using System.Diagnostics;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed class HeavyJobSystem : ITickSystemExecutor, ITimeRecorded
{
    public const int ExecutionElapsedTime = 200;
    public long ElapsedTime { get; private set; }

    public void Execute(TickSystemCommand command)
    {
        var sw = new Stopwatch();
        sw.Start();
        Thread.Sleep(TimeSpan.FromMilliseconds(HeavyJobSystem.ExecutionElapsedTime));
        sw.Stop();
        ElapsedTime = sw.ElapsedMilliseconds;
    }
}