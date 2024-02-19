﻿using Deepslate.Ecs.SourceGenerator;

namespace Deepslate.Ecs.Test.TestTickSystems;

public sealed partial class RecordDestroySystem : ITickSystemExecutor
{
    [WithIncluded<Position>] [RequireInstantCommand] [AsGenericQuery]
    private Query _query;

    private readonly CommandBuffer _commandBuffer;

    public RecordDestroySystem(TickSystemBuilder builder)
    {
        InitializeQuery(builder);
        builder.Build(this, out var tickSystem);
        _commandBuffer = new CommandBuffer(tickSystem);
    }

    public void Execute(EntityCommand command)
    {
        List<Entity> entities = [];
        foreach (var entityComponentBundle in _queryGeneric)
        {
            entities.Add(entityComponentBundle.Entity);
        }

        command.RecordDestroy(_commandBuffer, entities);
        _commandBuffer.Execute();
    }
}