namespace Deepslate.Ecs.Extensions;

public delegate void ReactAfterAlloc<TComponent>(Span<TComponent> component)
    where TComponent : IComponentData;
    
public delegate void ReactBeforeFree<TComponent>(Span<TComponent> component)
    where TComponent : IComponentData;
    
public delegate void ReactAfterCreate<TComponent>(Span<TComponent> component)
    where TComponent : IComponentData;
    
public delegate void ReactBeforeDestroy<TComponent>(ref TComponent component)
    where TComponent : IComponentData;
    
public delegate void ReactBeforeMove<TComponent>(ref TComponent from, ref TComponent to)
    where TComponent : IComponentData;

internal sealed record AfterAllocReactSystem<TComponent>(ReactAfterAlloc<TComponent> ReactAfterAlloc)
    : IReactAfterAlloc<TComponent>
    where TComponent : IComponentData
{
    public void AfterAlloc(Span<TComponent> component) => ReactAfterAlloc(component);
}

internal sealed record BeforeFreeReactSystem<TComponent>(ReactBeforeFree<TComponent> ReactBeforeFree)
    : IReactBeforeFree<TComponent>
    where TComponent : IComponentData
{
    public void BeforeFree(Span<TComponent> component) => ReactBeforeFree(component);
}

internal sealed record AfterCreateReactSystem<TComponent>(ReactAfterCreate<TComponent> ReactAfterCreate)
    : IReactAfterCreate<TComponent>
    where TComponent : IComponentData
{
    public void AfterCreate(Span<TComponent> component) => ReactAfterCreate(component);
}

internal sealed record BeforeDestroyReactSystem<TComponent>(ReactBeforeDestroy<TComponent> ReactBeforeDestroy)
    : IReactBeforeDestroy<TComponent>
    where TComponent : IComponentData
{
    public void BeforeDestroy(ref TComponent component) => ReactBeforeDestroy(ref component);
}

internal sealed record BeforeMoveReactSystem<TComponent>(ReactBeforeMove<TComponent> ReactBeforeMove)
    : IReactBeforeMove<TComponent>
    where TComponent : IComponentData
{
    public void BeforeMove(ref TComponent from, ref TComponent to) => ReactBeforeMove(ref from, ref to);
}