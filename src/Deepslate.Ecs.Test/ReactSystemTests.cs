using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.TestTickSystems;
using Xunit.Abstractions;

namespace Deepslate.Ecs.Test;

public sealed class ReactSystemTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public void AfterAlloc()
    {
        using var world = new WorldBuilder()
            .WithReactAfterAlloc<Position>(
                span => outputHelper.WriteLine($"{span.Length} {nameof(Position)} allocated"))
            .WithArchetypeAndBuild<Position, Velocity>(out var archetype)
            .AddStage(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                {
                    tickSystemBuilder.Build(new CreationSystem(tickSystemBuilder, 1));
                }).Build();
            }).Build();
        
        world.Tick();

        foreach (var entity in archetype.Entities)
        {
            // outputHelper.WriteLine($"{entity.Value}");
        }
    }
}