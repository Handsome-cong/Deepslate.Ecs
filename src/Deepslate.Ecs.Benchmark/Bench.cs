using BenchmarkDotNet.Attributes;
using Deepslate.Ecs.Benchmark.Ecs;
using Deepslate.Ecs.Benchmark.Oop;


namespace Deepslate.Ecs.Benchmark;

public class Bench
{
    private EcsApplication _ecsApplication;
    private OopApplication _oopApplication;

    private const int EntityCount = 4096;

    [GlobalSetup]
    public void Setup()
    {
        _ecsApplication = new EcsApplication { EntityCount = EntityCount };
        _oopApplication = new OopApplication { EntityCount = EntityCount };
        _ecsApplication.Prepare();
        _oopApplication.Prepare();
    }

    [Benchmark]
    public void UseOop()
    {
        _oopApplication.Start();
    }
    
    [Benchmark]
    public void UseEcs()
    {
        _ecsApplication.Start();
    }
}