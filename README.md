Deepslate.Ecs is a Entity Component System (ECS) library implemented in C# for Unity.
It is designed to be fast, flexible and easy to use.

**warning:** This library is still in development and is far from being ready for production.

## Features
- [X] Basic ECS Concepts
  - [x] `Entity` struct with just 64 bits representing your entities
  - [x] `IComponent` interface for tagging components
  - [x] `ISystem` interface for tagging systems
- [X] `Query` for iterating over entities with specific components
  - [x] Generic `Query` for explicit query requirements 
  - [x] Generic enumerator for generic query results
  - [ ] `ArchetypeCommand` for limitary access to entities
- [ ] `World` for managing entities, components and systems
- [ ] `Scheduler` for running systems in parallel

## Roadmap
- [ ] Generic support for `ArchetypeCommand`