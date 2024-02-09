using System.Buffers;

namespace Deepslate.Ecs;

internal sealed class ManagedComponentStorage<TComponent> (IComponentDataPoolFactory poolFactory)
    : IComponentDataStorage<TComponent>
    where TComponent : IComponentData
{
    private readonly IManagedComponentStoragePool<TComponent> _pool = poolFactory.CreateManagedPool<TComponent>();
    private IMemoryOwner<TComponent> _components = EmptyMemoryOwner<TComponent>.Instance;

    public int Count { get; private set; }
    
    public void Add(int count = 1)
    {
        var newCount = Count + count;
        if (newCount > _components.Memory.Length)
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

    public Span<TComponent> AsSpan() => _components.Memory.Span;

    public void Dispose()
    {
        _pool.Return(_components);
    }
}