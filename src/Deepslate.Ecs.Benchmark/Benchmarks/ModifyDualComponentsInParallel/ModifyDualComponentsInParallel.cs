using BenchmarkDotNet.Attributes;
using Deepslate.Ecs.Benchmark.Benchmarks.Common;

namespace Deepslate.Ecs.Benchmark.Benchmarks.ModifyDualComponentsInParallel;

[RPlotExporter]
[Config(typeof(BenchmarkConfig))]
public class ModifyDualComponentsInParallel
{
    private OopApplication _oopApplication;
    private DeepslateApplication _deepslateApplication;
    
    [Params(256, 1024, 4096)]
    public int EntityCount { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _oopApplication = new OopApplication { EntityCount = EntityCount };
        _oopApplication.Prepare();
        _deepslateApplication = new DeepslateApplication { EntityCount = EntityCount };
        _deepslateApplication.Prepare();
    }
    
    [GlobalCleanup]
    public void Cleanup()
    {
        _deepslateApplication.Dispose();
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
}