Deepslate.Ecs is a Entity Component System (ECS) library implemented in C#.
It is designed to be fast, flexible and easy to use.

**warning:** This library is still in development and is far from being ready for production.

## Roadmap
- [X] Basic ECS Concepts
  - [x] `Entity` struct with just 64 bits representing your entities
  - [x] `IComponent` interface for tagging components
  - [x] `ISystem` interface for tagging systems
- [ ] `Query` for iterating over entities with specific components
  - [x] Generic `QueryBuilder` for strongly typed query configuration
  - [x] Generic `Query` for explicit query requirements 
  - [x] Generic enumerator for generic query results
  - [ ] Source generator for generating query configuration code
- [ ] `Archetype` for storing entities and components
  - [x] Managed and unmanaged component storage
  - [x] `ArchetypeBuilder` for strongly typed archetype configuration
  - [ ] `ArchetypeCommand` for limitary access to entities
    - [x] Registration
    - [ ] Execution (not yet, because requires Scheduler support)
- [ ] `World` for managing entities, components and systems
  - [x] `WorldBuilder` for fluent world configuration
- [ ] `Scheduler` for running systems in parallel
