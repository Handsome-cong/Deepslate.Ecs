// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Deepslate.Ecs.Benchmark.Benchmarks.CreateEntity;
using Deepslate.Ecs.Benchmark.Benchmarks.ModifyDualComponentsInParallel;
using Deepslate.Ecs.Benchmark.Benchmarks.ModifySingleComponent;

BenchmarkRunner.Run<CreateEntity>();
// BenchmarkRunner.Run<ModifySingleComponent>();
// BenchmarkRunner.Run<ModifyDualComponentsInParallel>();