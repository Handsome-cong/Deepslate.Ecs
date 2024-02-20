using System.Buffers;
using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

public interface IManagedComponentStoragePool<TComponent> : IDisposable
    where TComponent : IComponent
{
    IMemoryOwner<TComponent> Rent(int minimumLength);
    void Return(IMemoryOwner<TComponent> memory);
}

public interface IUnmanagedComponentStoragePool : IDisposable
{
    IMemoryOwner<byte> Rent(int minimumLength);
    void Return(IMemoryOwner<byte> memory);
}

public interface IComponentPoolFactory
{
    IManagedComponentStoragePool<TComponent> CreateManagedPool<TComponent>() where TComponent : IComponent;

    IUnmanagedComponentStoragePool CreateUnmanagedPool<TComponent>()
        where TComponent : unmanaged, IComponent;
}

internal sealed class DefaultComponentPoolFactory : IComponentPoolFactory
{
    public IManagedComponentStoragePool<TComponent> CreateManagedPool<TComponent>()
        where TComponent : IComponent => new DefaultManagedComponentStoragePool<TComponent>();

    public IUnmanagedComponentStoragePool CreateUnmanagedPool<TComponent>()
        where TComponent : unmanaged, IComponent => new DefaultUnmanagedComponentStoragePool<TComponent>();

    private sealed class DefaultManagedComponentStoragePool<TComponent> : IManagedComponentStoragePool<TComponent>
        where TComponent : IComponent
    {
        public IMemoryOwner<TComponent> Rent(int minimumLength) => MemoryPool<TComponent>.Shared.Rent(minimumLength);

        public void Return(IMemoryOwner<TComponent> memory)
        {
            if (memory is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public void Dispose()
        {
        }
    }

    private sealed class DefaultUnmanagedComponentStoragePool<TComponent> : IUnmanagedComponentStoragePool
        where TComponent : unmanaged, IComponent
    {
        public IMemoryOwner<byte> Rent(int minimumLength)
        {
            var minimumSize = Unsafe.SizeOf<TComponent>() * minimumLength;
            var array = ArrayPool<byte>.Shared.Rent(minimumSize);
            return new UnmanagedMemoryOwner(array);
        }

        public void Return(IMemoryOwner<byte> memory)
        {
            if (memory is UnmanagedMemoryOwner unmanagedMemoryOwner)
            {
                ArrayPool<byte>.Shared.Return(unmanagedMemoryOwner.Array);
            }
        }

        public void Dispose()
        {
        }

        private sealed class UnmanagedMemoryOwner(byte[] array)
            : IMemoryOwner<byte>
        {
            public readonly byte[] Array = array;

            public Memory<byte> Memory => Array;

            public void Dispose()
            {
            }
        }
    }
}

internal class EmptyMemoryOwner<TComponent> : IMemoryOwner<TComponent>
{
    public static readonly EmptyMemoryOwner<TComponent> Instance = new();
    public Memory<TComponent> Memory => Memory<TComponent>.Empty;

    public void Dispose()
    {
    }
}