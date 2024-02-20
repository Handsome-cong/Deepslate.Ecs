using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Test.TestResources;
using Deepslate.Ecs.Test.TestTickSystems;

namespace Deepslate.Ecs.Test;

public sealed class ResourceTests
{
    [Fact]
    public void Register()
    {
        using var world = new WorldBuilder()
            .WithResource(new CounterResourceFactory().Create)
            .Build();
    }

    [Fact]
    public void GetFromCommand()
    {
        using var world = new WorldBuilder()
            .WithResource(new CounterResourceFactory().Create)
            .AddStageAndBuild(stageBuilder => stageBuilder.AddTickSystem(tickSystemBuilder =>
                tickSystemBuilder.Build(new ResourceSystem<CounterResource>(tickSystemBuilder))))
            .Build();

        world.Tick();
        var command = world.CreateGlobalCommand();
        var counter = command.GetResource<CounterResource>();
        Assert.Equal(1, counter.Value);
    }

    [Fact]
    public void ExceptionWhenNotRegistered()
    {
        var worldBuilder = new WorldBuilder()
            .AddStageAndBuild(stageBuilder => stageBuilder.AddTickSystem(tickSystemBuilder =>
                tickSystemBuilder.Build(new ResourceSystem<CounterResource>(tickSystemBuilder))));

        Assert.Throws<InvalidOperationException>(() => worldBuilder.Build());

        using var world = new WorldBuilder().Build();
        var command = world.CreateGlobalCommand();
        Assert.Throws<InvalidOperationException>(() => command.GetResource<CounterResource>());
    }

    [Fact]
    public async void ExceptionWhenNotRequired()
    {
        using var world = new WorldBuilder()
            .WithResource(new CounterResourceFactory().Create)
            .AddStageAndBuild(stageBuilder => stageBuilder.AddTickSystem(tickSystemBuilder =>
                tickSystemBuilder.Build(new ResourceSystemRetrieveOnly<CounterResource>(tickSystemBuilder))))
            .Build();

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await world.TickAsync());
    }
}