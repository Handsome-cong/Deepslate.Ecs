using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.Extensions;
using Deepslate.Ecs.Test.TestResources;
using Deepslate.Ecs.Test.TestTickSystems;

namespace Deepslate.Ecs.Test;

public sealed class ConflictTests
{
    [Fact]
    public void ByResource()
    {
        TickSystem system1 = default!;
        TickSystem system2 = default!;
        TickSystem system3 = default!;
        using var world = new WorldBuilder()
            .WithResource(new CounterResourceFactory().Create)
            .WithResource(new EmptyResource())
            .AddStageAndBuild(stageBuilder =>
            {
                stageBuilder.AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new ResourceSystem<EmptyResource>(tickSystemBuilder), out system1))
                    .AddTickSystem(tickSystemBuilder =>
                        tickSystemBuilder.Build(new ResourceSystem<EmptyResource>(tickSystemBuilder), out system2))
                    .AddTickSystem(tickSystemBuilder =>
                    tickSystemBuilder.Build(new ResourceSystem<CounterResource>(tickSystemBuilder), out system3));
            }).Build();
        
        Assert.True(IsConflict(system1, system2));
        Assert.False(IsConflict(system1, system3));
        Assert.False(IsConflict(system2, system3));
    }
    
    private static bool IsConflict(TickSystem system1, TickSystem system2)
    {
        var usageCodeBundle = system1.CreateUsageCodeBundle();
        var usageCodeBundle2 = system2.CreateUsageCodeBundle();
        return usageCodeBundle.ConflictWith(usageCodeBundle2);
    }
}