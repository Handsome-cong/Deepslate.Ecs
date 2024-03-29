![example workflow](https://github.com/Handsome-cong/Deepslate.Ecs/actions/workflows/dotnet.yml/badge.svg)

Deepslate.Ecs is a Entity Component System (ECS) library implemented in C#.
It is designed to be fast, flexible and easy to use.

**warning:** This library is still in development and is far from being ready for production.

## Roadmap
- [x] `Entity` struct with just 64 bits representing your entities
- [x] `IComponentData` interface for tagging components
- [x] `ISystemExecutor` interface for defining systems that run every tick
- [x] `Query` for iterating over entities with specific components
  - [x] Generic `QueryBuilder` for strongly typed query configuration
  - [x] Generic `Query` for explicit query requirements 
  - [x] Generic enumerator for generic query results
  - [x] Source generator for generating query configuration code
- [x] `Archetype` for storing entities and components
  - [x] Managed and unmanaged component storage
  - [x] `ArchetypeBuilder` for strongly typed archetype configuration
  - [x] `ArchetypeCommand` for limitary access to entities
- [x] `World` for managing entities, components and systems
  - [x] `WorldBuilder` for fluent world configuration
- [x] `Scheduler` for running systems in parallel
  - [x] `Stage` for grouping systems
  - [x] Automatic parallelization of systems with no conflicting queries
  - [x] Execution of deferred command created by `EntityCommand`
- [x] `EntityCommand` for creating, destroying and modifying entities and components in tick
- [x] `GlobalCommand` for creating, destroying and modifying entities and components out of tick
- [x] `Resource` for global data
- [X] Reactive systems for handling events

## Api Conventions
- `With` prefix for methods that add or overwrite something, if the arguments of multiple calls are the same or partially the same.
- `Add` prefix for methods that may add something multiple times, even if the arguments are the same.
- Additional arguments for `build` methods of builders mean that the arguments are not optional and must be provided.

## How To Build
### Prerequisites
- .NET 8.0 SDK
- .NET Compiler Platform SDK (Roslyn)

### Visual Studio / Rider
Just open the solution and build it. You may need to execute the t4 templates manually.

### Powershell
Run the build script in the root directory of the repository.
```powershell
./build.ps1
```

If you run this script in visual studio developer powershell, the "TextTransform.exe" will be used to execute the t4 templates.
Otherwise, "dotnet-t4" will be installed as a local tool and do the job.

### Benchmark
Here are some benchmarks comparing Deepslate.Ecs with traditional OOP implementations.

#### Create Entities
Create 256, 1024, 4096 entities with single component.
![CreateEntitiesBenchmark](docs/benchmarks/Deepslate.Ecs.Benchmark.Benchmarks.CreateEntity.CreateEntity-barplot.png)

#### Modify Components
Modify single component of 256, 1024, 4096 entities.
![ModifyComponentsBenchmark](docs/benchmarks/Deepslate.Ecs.Benchmark.Benchmarks.ModifySingleComponent.ModifySingleComponent-barplot.png)

#### Modify Components In Parallel
Modify two components of 256, 1024, 4096 entities in parallel. Auto parallelization in Deepslate.Ecs and manual parallelization in OOP.
![ModifyComponentsInParallelBenchmark](docs/benchmarks/Deepslate.Ecs.Benchmark.Benchmarks.ModifyDualComponentsInParallel.ModifyDualComponentsInParallel-barplot.png)