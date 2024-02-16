using System.Buffers;

namespace Deepslate.Ecs;

internal sealed class ManagedComponentStorage<TComponent>(WorldBuilder builder)
    : IComponentDataStorage<TComponent>
    where TComponent : IComponentData
{
    private readonly IManagedComponentStoragePool<TComponent> _pool =
        builder.ComponentPoolFactory.CreateManagedPool<TComponent>();

    private IMemoryOwner<TComponent> _components = EmptyMemoryOwner<TComponent>.Instance;

    public IReactAfterAlloc<TComponent>? ReactAfterAlloc { get; } =
        builder.ReactAfterAlloc!.GetValueOrDefault(typeof(TComponent), null) as IReactAfterAlloc<TComponent>;

    public IReactBeforeFree<TComponent>? ReactBeforeFree { get; } =
        builder.ReactBeforeFree!.GetValueOrDefault(typeof(TComponent), null) as IReactBeforeFree<TComponent>;

    public IReactAfterCreate<TComponent>? ReactAfterCreate { get; } =
        builder.ReactAfterCreate!.GetValueOrDefault(typeof(TComponent), null) as IReactAfterCreate<TComponent>;

    public IReactBeforeDestroy<TComponent>? ReactBeforeDestroy { get; } =
        builder.ReactBeforeDestroy!.GetValueOrDefault(typeof(TComponent), null) as IReactBeforeDestroy<TComponent>;

    public IReactBeforeMove<TComponent>? ReactBeforeMove { get; } =
        builder.ReactBeforeMove!.GetValueOrDefault(typeof(TComponent), null) as IReactBeforeMove<TComponent>;

    public int Count { get; private set; }

    public void Add(int count = 1)
    {
        var newCount = Count + count;
        if (newCount > _components.Memory.Length)
        {
            var newComponents = _pool.Rent(newCount);
            var oldSpan = _components.Memory.Span;
            var newSpan = newComponents.Memory.Span;
            ReactBeforeMove?.BeforeMove(oldSpan[..Count], newSpan[..Count]);
            oldSpan.CopyTo(newSpan);
            ReactAfterAlloc?.AfterAlloc(newSpan[newCount..]);
            ReactBeforeFree?.BeforeFree(oldSpan);
            _pool.Return(_components);
            _components = newComponents;
        }

        Count = newCount;
        ReactAfterCreate?.AfterCreate(AsSpan().Slice(Count - count, count));
    }

    public void Pop(int count = 1)
    {
        if (count > 0)
        {
            Count -= count;
        }
    }

    public Span<TComponent> AsSpan() => _components.Memory.Span;
    public Memory<TComponent> AsMemory() => _components.Memory;

    public void Dispose()
    {
        _pool.Return(_components);
    }
}