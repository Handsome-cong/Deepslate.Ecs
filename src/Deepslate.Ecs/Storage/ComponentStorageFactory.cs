using System.Reflection;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

internal sealed class ComponentStorageFactory
{
    internal readonly Dictionary<Type, Func<WorldBuilder, IComponentDataStorage>> Factories = [];
    
    
    internal void RegisterManagedFactory<TComponent>()
        where TComponent : IComponentData
    {
        Factories[typeof(TComponent)] = builder => new ManagedComponentStorage<TComponent>(builder);
    }

    internal void RegisterUnmanagedFactory<TComponent>()
        where TComponent : unmanaged, IComponentData
    {
        Factories[typeof(TComponent)] = builder => new UnmanagedComponentStorage<TComponent>(builder);
    }

    private static readonly MethodInfo TypelessRegisterManagedFactory = typeof(ComponentStorageFactory)
        .GetMethod(nameof(RegisterManagedFactory),
            BindingFlags.Instance | BindingFlags.NonPublic,
            Array.Empty<Type>())!;

    internal void RegisterManagedFactory(Type componentType)
    {
        Guard.IsComponent(componentType);
        TypelessRegisterManagedFactory.MakeGenericMethod(componentType)
            .Invoke(this, null);
    }

    private static readonly MethodInfo TypelessRegisterUnmanagedFactory =
        typeof(ComponentStorageFactory)
            .GetMethod(nameof(RegisterUnmanagedFactory),
                BindingFlags.Instance | BindingFlags.NonPublic,
                Array.Empty<Type>())!;

    internal void RegisterUnmanagedFactory(Type componentType)
    {
        Guard.IsComponent(componentType);
        Guard.IsUnmanaged(componentType);
        TypelessRegisterUnmanagedFactory.MakeGenericMethod(componentType)
            .Invoke(this, null);
    }
}