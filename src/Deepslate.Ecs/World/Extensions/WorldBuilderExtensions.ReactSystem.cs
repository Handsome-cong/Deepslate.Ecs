namespace Deepslate.Ecs.Extensions;

public static partial class WorldBuilderExtensions
{
    public static WorldBuilder WithReactAfterAlloc<TComponent>(
        this WorldBuilder builder,
        ReactAfterAlloc<TComponent> executor)
        where TComponent : IComponent
    {
        return builder.WithReactAfterAlloc(new AfterAllocReactSystem<TComponent>(executor));
    }
    
    public static WorldBuilder WithReactBeforeFree<TComponent>(
        this WorldBuilder builder,
        ReactBeforeFree<TComponent> executor)
        where TComponent : IComponent
    {
        return builder.WithReactBeforeFree(new BeforeFreeReactSystem<TComponent>(executor));
    }
    
    public static WorldBuilder WithReactAfterCreate<TComponent>(
        this WorldBuilder builder,
        ReactAfterCreate<TComponent> executor)
        where TComponent : IComponent
    {
        return builder.WithReactAfterCreate(new AfterCreateReactSystem<TComponent>(executor));
    }
    
    public static WorldBuilder WithReactBeforeDestroy<TComponent>(
        this WorldBuilder builder,
        ReactBeforeDestroy<TComponent> executor)
        where TComponent : IComponent
    {
        return builder.WithReactBeforeDestroy(new BeforeDestroyReactSystem<TComponent>(executor));
    }
    
    public static WorldBuilder WithReactBeforeMove<TComponent>(
        this WorldBuilder builder,
        ReactBeforeMove<TComponent> executor)
        where TComponent : IComponent
    {
        return builder.WithReactBeforeMove(new BeforeMoveReactSystem<TComponent>(executor));
    }
}