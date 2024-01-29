namespace Deepslate.Ecs;

internal interface IComponentStorage
{
    internal const int SizeOfPage = 1024;

    Type ComponentType { get; }
    int Count { get; }
    
    void Add(int count = 1);
    void Pop(int count = 1);
    void Remove(int index);
}

internal interface IComponentStorage<TComponent> : IComponentStorage
    where TComponent : IComponent
{
    
    Type IComponentStorage.ComponentType => typeof(TComponent);

    Span<TComponent> Add(int count = 1);
    Span<TComponent> Pop(int count = 1);

    Span<TComponent> AsSpan();
    
    ref TComponent this[int index] => ref AsSpan()[index];

    void IComponentStorage.Add(int count) => Add(count);
    void IComponentStorage.Pop(int count) => Pop(count);
    void IComponentStorage.Remove(int index)
    {
        var count = Count;
        if (index < 0 || index >= count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        }
        if (index < count - 1)
        {
            var span = AsSpan();
            span[index] = span[count - 1];
        }
        Pop();
    }
}

internal static class ComponentStorageExtensions
{
    public static void Swap<TComponent>(this IComponentStorage<TComponent> storage, int from, int to)
        where TComponent : IComponent
    {
        ref var a = ref storage[from];
        ref var b = ref storage[to];
        (a, b) = (b, a);
    }
}
