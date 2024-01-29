using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Deepslate.Ecs;

namespace Deepslate.Ecs;

internal sealed class UnmanagedComponentStorage<TComponent> : IComponentStorage<TComponent>, IDisposable
    where TComponent : unmanaged, IComponent
{
    private const int SizeOfPage = IComponentStorage.SizeOfPage;

    private unsafe TComponent* _components = null;
    private int _length;

    public int Count { get; private set; }

    public unsafe Span<TComponent> Add(int count = 1)
    {
        var newCount = Count + count;
        if (newCount > _length)
        {
            var newLength = (newCount / SizeOfPage + 1) * SizeOfPage;
            var newComponents = (TComponent*)NativeMemory.Alloc((nuint)(newLength * Unsafe.SizeOf<TComponent>()));
            Unsafe.CopyBlock(newComponents, _components, (uint)(Count * Unsafe.SizeOf<TComponent>()));
            NativeMemory.Free(_components);
            _components = newComponents;
            _length = newLength;
        }

        Count = newCount;
        return new Span<TComponent>(_components + Count - count, count);
    }

    public unsafe Span<TComponent> Pop(int count = 1)
    {
        if (count > 0)
        {
            Count -= count;
        }

        return new Span<TComponent>(_components + Count, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Span<TComponent> AsSpan() => new(_components, Count);

    public unsafe void Dispose()
    {
        NativeMemory.Free(_components);
    }
}
