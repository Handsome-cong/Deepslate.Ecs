﻿using Deepslate.Ecs.Benchmark.Svelto.Components;
using Svelto.ECS;

namespace Deepslate.Ecs.Benchmark.Svelto.Engines;

public sealed class MovementEngine(ExclusiveGroup group) : IQueryingEntitiesEngine
{
    private ExclusiveGroup _group = group;
    
    public EntitiesDB entitiesDB { get; set; } = default!;

    public void Ready()
    {
    }

    public void Update()
    {
        var (buffer, _, count)= entitiesDB.QueryEntities<Position>(_group);
        for (var i = 0; i < count; i++)
        {
            ref var position = ref buffer[i];
            position.X++;
            position.Y++;
            position.Z++;
        }
    }
}