using System.Reflection;
using System.Runtime.CompilerServices;

namespace Deepslate.Ecs.Util;

internal static class ArrayFactory
{
    internal static readonly
        Dictionary<Type, Func<IEnumerable<IComponentDataStorage>, IReadOnlyList<IComponentDataStorage>>>
        StorageArrayFactories = [];

    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static void RegisterStorageArrayFactory<TComponent>()
        where TComponent : IComponentData
    {
        StorageArrayFactories.TryAdd(
            typeof(TComponent),
            storages => storages.Cast<IComponentDataStorage<TComponent>>().ToArray());
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static void RegisterStorageArrayFactory(Type type)
    {
        Guard.IsComponent(type);
        typeof(ArrayFactory)
            .GetMethod(nameof(RegisterStorageArrayFactory), BindingFlags.Static | BindingFlags.NonPublic, Array.Empty<Type>())
            !.MakeGenericMethod(type)
            .Invoke(null, null);
    }
}