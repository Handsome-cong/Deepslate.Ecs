namespace Deepslate.Ecs;

public interface IReactSystemExecutor;

public interface IReactAfterAlloc<TComponent> : IReactSystemExecutor
    where TComponent : IComponent
{
    void AfterAlloc(Span<TComponent> component);
}

public interface IReactBeforeFree<TComponent> : IReactSystemExecutor
    where TComponent : IComponent
{
    void BeforeFree(Span<TComponent> component);
}

public interface IReactAfterCreate<TComponent> : IReactSystemExecutor
    where TComponent : IComponent
{
    void AfterCreate(Span<TComponent> component);
}

public interface IReactBeforeDestroy<TComponent> : IReactSystemExecutor
    where TComponent : IComponent
{
    void BeforeDestroy(ref TComponent component);
}

public interface IReactBeforeMove<TComponent> : IReactSystemExecutor
    where TComponent : IComponent
{
    void BeforeMove(Span<TComponent> from, Span<TComponent> to);
}