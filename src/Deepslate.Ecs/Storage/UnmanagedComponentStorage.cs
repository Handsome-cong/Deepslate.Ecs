using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deepslate.Ecs;

internal sealed class UnmanagedComponentStorage<TComponent>(WorldBuilder builder)
    : IComponentDataStorage<TComponent>
    where TComponent : unmanaged, IComponentData
{
    private readonly IUnmanagedComponentStoragePool _pool =
        builder.ComponentPoolFactory.CreateUnmanagedPool<TComponent>();

    private IMemoryOwner<byte> _components = EmptyMemoryOwner<byte>.Instance;

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
        if (newCount > AsSpan().Length)
        {
            var newComponents = _pool.Rent(newCount);
            var oldSpan = _components.Memory.Span;
            var newSpan = newComponents.Memory.Span;
            var typedOldSpan = MemoryMarshal.Cast<byte, TComponent>(oldSpan);
            var typedNewSpan = MemoryMarshal.Cast<byte, TComponent>(newSpan);
            ReactBeforeMove?.BeforeMove(typedOldSpan[..Count], typedNewSpan[..Count]);
            oldSpan.CopyTo(newSpan);
            ReactAfterAlloc?.AfterAlloc(typedNewSpan[newCount..]);
            ReactBeforeFree?.BeforeFree(typedOldSpan);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<TComponent> AsSpan() => MemoryMarshal.Cast<byte, TComponent>(_components.Memory.Span);

    public Memory<TComponent> AsMemory()
    {
        var resizeFactor = Unsafe.SizeOf<TComponent>() / sizeof(byte);
        var typelessMemory = _components.Memory;
        return Unsafe.As<Memory<byte>, Memory<TComponent>>(ref typelessMemory)
            [..(_components.Memory.Length / resizeFactor)];
    }

    public void Dispose()
    {
        _pool.Return(_components);
    }
}