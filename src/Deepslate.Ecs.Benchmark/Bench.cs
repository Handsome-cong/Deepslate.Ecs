using BenchmarkDotNet.Attributes;
using Deepslate.Ecs.Benchmark.Deepslate;
using Deepslate.Ecs.Benchmark.Flecs;
using Deepslate.Ecs.Benchmark.Oop;


namespace Deepslate.Ecs.Benchmark;

public class Bench
{
    private DeepslateApplication _deepslateApplication;
    private OopApplication _oopApplication;
    private FlecsApplication _flecsApplication;

    private const int EntityCount = 1024;

    [GlobalSetup]
    public void Setup()
    {
        _deepslateApplication = new DeepslateApplication { EntityCount = EntityCount };
        _deepslateApplication.Prepare();
        _oopApplication = new OopApplication { EntityCount = EntityCount };
        _oopApplication.Prepare();
        _flecsApplication = new FlecsApplication { EntityCount = EntityCount };
        _flecsApplication.Prepare();
    }

    [Benchmark]
    public void UseOop()
    {
        _oopApplication.Start();
    }
    
    [Benchmark]
    public void UseDeepslate()
    {
        _deepslateApplication.Start();
    }

    [Benchmark]
    public void UseFlecs()
    {
        _flecsApplication.Start();
    }
}