using System.ComponentModel;
using BenchmarkDotNet.Attributes;
using Deepslate.Ecs.Benchmark.Benchmarks.Common;

namespace Deepslate.Ecs.Benchmark.Benchmarks.CreateEntity;

[RPlotExporter]
[Config(typeof(BenchmarkConfig))]
public class CreateEntity
{
    private OopApplication _oopApplication;
    private DeepslateApplication _deepslateApplication;
    
    [Params(256, 1024, 4096)]
    public int EntityCount { get; set; }
    
    [IterationSetup]
    public void Setup()
    {
        _oopApplication = new OopApplication { EntityCount = EntityCount };
        _oopApplication.Prepare();
        _deepslateApplication = new DeepslateApplication { EntityCount = EntityCount };
        _deepslateApplication.Prepare();
    }
    
    [IterationCleanup]
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