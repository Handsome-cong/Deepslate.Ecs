using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deepslate.Ecs;

internal sealed class UnmanagedComponentStorage<TComponent>(IComponentPoolFactory poolFactory)
    : IComponentStorage<TComponent>, IDisposable
    where TComponent : unmanaged, IComponent
{
    private readonly IUnmanagedComponentStoragePool _pool = poolFactory.CreateUnmanagedPool<TComponent>();

    private IMemoryOwner<byte> _components = EmptyMemoryOwner<byte>.Instance;

    public int Count { get; private set; }

    public void Add(int count = 1)
    {
        var newCount = Count + count;
        if (newCount > AsSpan().Length)
        {
            var newComponents = _pool.Rent(newCount);
            _components.Memory.CopyTo(newComponents.Memory);
            _pool.Return(_components);
            _components = newComponents;
        }

        Count = newCount;
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

    public void Dispose()
    {
        _pool.Return(_components);
    }
}