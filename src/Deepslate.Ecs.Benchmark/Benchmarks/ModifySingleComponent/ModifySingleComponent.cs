using BenchmarkDotNet.Attributes;
using Deepslate.Ecs.Benchmark.Benchmarks.Common;

namespace Deepslate.Ecs.Benchmark.Benchmarks.ModifySingleComponent;

[RPlotExporter]
[Config(typeof(BenchmarkConfig))]
public class ModifySingleComponent
{
    private DeepslateApplication _deepslateApplication;
    private OopApplication _oopApplication;
    private FlecsApplication _flecsApplication;
    private SveltoApplication _sveltoApplication;

    [Params(256, 1024, 4096)]
    public int EntityCount { get; set; }

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

    [GlobalCleanup]
    public void Cleanup()
    {
        _deepslateApplication.Dispose();
        _sveltoApplication.Dispose();
    }

    [Benchmark]
    public void Oop()
    {
        _oopApplication.Start();
    }
    
    [Benchmark]
    public void Deepslate()
    {
        _deepslateApplication.Start();
    }

    public void Flecs()
    {
        _flecsApplication.Start();
    }
    
    public void Svelto()
    {
        _sveltoApplication.Start();
    }
}