Deepslate.Ecs is a Entity Component System (ECS) library implemented in C#.
It is designed to be fast, flexible and easy to use.

**warning:** This library is still in development and is far from being ready for production.

## Roadmap
- [X] Basic ECS Concepts
  - [x] `Entity` struct with just 64 bits representing your entities
  - [x] `IComponentData` interface for tagging components
  - [x] `ITickSystem` interface for tagging systems
- [ ] `Query` for iterating over entities with specific components
  - [x] Generic `QueryBuilder` for strongly typed query configuration
  - [x] Generic `Query` for explicit query requirements 
  - [x] Generic enumerator for generic query results
  - [ ] Source generator for generating query configuration code
- [x] `Archetype` for storing entities and components
  - [x] Managed and unmanaged component storage
  - [x] `ArchetypeBuilder` for strongly typed archetype configuration
  - [x] `ArchetypeCommand` for limitary access to entities
- [x] `World` for managing entities, components and systems
  - [x] `WorldBuilder` for fluent world configuration
- [X] `Scheduler` for running systems in parallel
  - [x] `Stage` for grouping systems
  - [x] Automatic parallelization of systems with no conflicting queries
  - [x] Execution of `ArchetypeCommand`

## Api Conventions
- `With` prefix for methods that add or overwrite something, if the arguments of multiple calls are the same or partially the same.
- `Add` prefix for methods that may add something multiple times, even if the arguments are the same.
- Additional arguments for `build` methods of builders mean that the arguments are not optional and must be provided.