namespace Deepslate.Ecs.Benchmark.Benchmarks.Common;

public interface IBenchmarkApplication : IDisposable
{
    void Prepare();
    void Start();

    void IDisposable.Dispose()
    {
    }
}