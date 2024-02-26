using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;

namespace Deepslate.Ecs.Benchmark.Benchmarks.Common;

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddExporter(CsvMeasurementsExporter.Default);
        AddExporter(RPlotExporter.Default);
    }
}