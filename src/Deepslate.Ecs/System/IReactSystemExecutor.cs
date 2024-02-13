namespace Deepslate.Ecs;

public interface IReactSystemExecutor;

public interface IReactAfterAlloc<TComponent> : IReactSystemExecutor
    where TComponent : IComponentData
{
    void AfterAlloc(Span<TComponent> component);
}

public interface IReactBeforeFree<TComponent> : IReactSystemExecutor
    where TComponent : IComponentData
{
    void BeforeFree(Span<TComponent> component);
}

public interface IReactAfterCreate<TComponent> : IReactSystemExecutor
    where TComponent : IComponentData
{
    void AfterCreate(Span<TComponent> component);
}

public interface IReactBeforeDestroy<TComponent> : IReactSystemExecutor
    where TComponent : IComponentData
{
    void BeforeDestroy(ref TComponent component);
}

public interface IReactBeforeMove<TComponent> : IReactSystemExecutor
    where TComponent : IComponentData
{
    void BeforeMove(ref TComponent from, ref TComponent to);
}