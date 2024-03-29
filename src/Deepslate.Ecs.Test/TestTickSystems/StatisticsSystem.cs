﻿using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class StatisticsSystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [AsGenericQuery]
    private Query _query;
    
    public int Count { get; private set; }
    
    public StatisticsSystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
    }
    
    public void Execute(Command command)
    {
        Count = 0;
        foreach (var storageSpan in _queryGeneric.Storages)
        {
            Count += storageSpan.Count;
        }
    }
}