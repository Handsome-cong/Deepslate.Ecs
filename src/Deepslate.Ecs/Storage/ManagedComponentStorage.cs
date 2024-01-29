namespace Deepslate.Ecs;

internal sealed class ManagedComponentStorage<TComponent> : IComponentStorage<TComponent>
    where TComponent : IComponent
{
    private const int SizeOfPage = IComponentStorage.SizeOfPage;

    private TComponent[] _components = Array.Empty<TComponent>();

    public int Count { get; private set; }
    
    public Span<TComponent> Add(int count = 1)
    {
        var newCount = Count + count;
        if (newCount > _components.Length)
        {
            var newLength = (newCount / SizeOfPage + 1) * SizeOfPage;
            Array.Resize(ref _components, newLength);
        }
        Count = newCount;
        return _components.AsSpan(Count - count, count);
    }

    public Span<TComponent> Pop(int count = 1)
    {
        if (count > 0)
        {
            Count -= count;
        }
        return _components.AsSpan(Count, count);
    }

    public Span<TComponent> AsSpan() => _components;
}
