using BenchmarkDotNet.Attributes;
using Deepslate.Ecs.Benchmark.Deepslate;
using Deepslate.Ecs.Benchmark.Flecs;
using Deepslate.Ecs.Benchmark.Oop;
using Deepslate.Ecs.Benchmark.Svelto;


namespace Deepslate.Ecs.Benchmark;

public class Bench
{
    private DeepslateApplication _deepslateApplication;
    private OopApplication _oopApplication;
    private FlecsApplication _flecsApplication;
    private SveltoApplication _sveltoApplication;

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
        _sveltoApplication = new SveltoApplication { EntityCount = EntityCount };
        _sveltoApplication.Prepare();
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
    
    [Benchmark]
    public void UseSvelto()
    {
        _sveltoApplication.Start();
    }
}