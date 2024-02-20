namespace Deepslate.Ecs;

public interface IComponentStorage : IDisposable
{
    /// <summary>
    /// The size of expanded space when the space isn't enough.
    /// </summary>
    internal const int SizeOfPage = 1024;

    // Currently, the components with the same type in the same archetype are stored in a contiguous memory block.
    // That means a huge memory copy and allocation will happen when the space is not enough, if there are thousands of entities.
    // Maybe separate the components into pages will be better.

    Type ComponentType { get; }
    int Count { get; }

    void Add(int count = 1);
    void Pop(int count = 1);
    void Remove(int index);
    void RemoveMany(Span<int> sortedIndices);
}

public interface IComponentStorage<TComponent> : IComponentStorage
    where TComponent : IComponent
{
    Type IComponentStorage.ComponentType => typeof(TComponent);

    protected IReactBeforeDestroy<TComponent>? ReactBeforeDestroy => null;
    protected IReactBeforeMove<TComponent>? ReactBeforeMove => null;

    new void Add(int count = 1);
    new void Pop(int count = 1);

    Span<TComponent> AsSpan();
    Memory<TComponent> AsMemory();

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

        var span = AsSpan();
        ReactBeforeDestroy?.BeforeDestroy(ref span[index]);
        if (index < count - 1)
        {
            var from = span.Slice(count - 1, 1);
            var to = span.Slice(index, 1);
            ReactBeforeMove?.BeforeMove(from, to);
            to[0] = from[0];
        }

        Pop();
    }

    void IComponentStorage.RemoveMany(Span<int> sortedIndices)
    {
        if (sortedIndices.Length == 0)
        {
            return;
        }

        var count = Count;
        var reservedIndicesStack = new int[count - sortedIndices[0] - sortedIndices.Length];
        var reservedIndicesCount = 0;
        for (var i = 1; i < sortedIndices.Length; i++)
        {
            for (var j = sortedIndices[i - 1] + 1; j < sortedIndices[i]; j++)
            {
                reservedIndicesStack[reservedIndicesCount++] = j;
            }
        }

        for (var i = sortedIndices[^1] + 1; i < count; i++)
        {
            reservedIndicesStack[reservedIndicesCount++] = i;
        }

        var span = AsSpan();
        foreach (var i in sortedIndices)
        {
            ReactBeforeDestroy?.BeforeDestroy(ref span[i]);
        }

        for (var i = 0;
             i < sortedIndices.Length && reservedIndicesCount > 0 &&
             reservedIndicesStack[reservedIndicesCount - 1] > sortedIndices[i];
             i++)
        {
            var from = span.Slice(reservedIndicesStack[--reservedIndicesCount], 1);
            var to = span.Slice(sortedIndices[i], 1);
            ReactBeforeMove?.BeforeMove(from, to);
            to[0] = from[0];
        }

        Pop(sortedIndices.Length);
    }
}