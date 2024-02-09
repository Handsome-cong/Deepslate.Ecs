using System.Reflection;
using System.Runtime.CompilerServices;

namespace Deepslate.Ecs.Util;

internal static class ComponentStorageFactory
{
    internal static readonly Dictionary<Type, Func<IComponentDataPoolFactory, IComponentDataStorage>>
        ComponentStorageFactories = new();

    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static void RegisterManagedComponentStorageFactory<TComponent>()
        where TComponent : IComponentData
    {
        ComponentStorageFactories.TryAdd(typeof(TComponent),
            poolFactory => new ManagedComponentStorage<TComponent>(poolFactory));
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static void RegisterUnmanagedComponentStorageFactory<TComponent>()
        where TComponent : unmanaged, IComponentData
    {
        RegisterManagedComponentStorageFactory<TComponent>();
        ComponentStorageFactories.TryAdd(typeof(TComponent),
            poolFactory => new UnmanagedComponentStorage<TComponent>(poolFactory));
    }

    private static readonly MethodInfo TypelessRegisterManagedComponentStorageFactory = typeof(ComponentStorageFactory)
        .GetMethod(nameof(RegisterManagedComponentStorageFactory),
            BindingFlags.Static | BindingFlags.NonPublic,
            Array.Empty<Type>())!;

    internal static void RegisterManagedComponentStorageFactory(Type componentType)
    {
        Guard.IsComponent(componentType);
        TypelessRegisterManagedComponentStorageFactory.MakeGenericMethod(componentType)
            .Invoke(null, null);
    }

    private static readonly MethodInfo TypelessRegisterUnmanagedComponentStorageFactory =
        typeof(ComponentStorageFactory)
            .GetMethod(nameof(RegisterUnmanagedComponentStorageFactory),
                BindingFlags.Static | BindingFlags.NonPublic,
                Array.Empty<Type>())!;

    internal static void RegisterUnmanagedComponentStorageFactory(Type componentType)
    {
        Guard.IsComponent(componentType);
        Guard.IsUnmanaged(componentType);
        TypelessRegisterUnmanagedComponentStorageFactory.MakeGenericMethod(componentType)
            .Invoke(null, null);
    }
}