using System.Reflection;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

/// <summary>
/// This is mainly used to convert <see cref="IComponentDataStorage"/>[]
/// to <see cref="IComponentDataStorage{TComponent}"/>[].
/// </summary>
internal sealed class StorageArrayFactory
{
    internal readonly
        Dictionary<Type, Func<IEnumerable<IComponentDataStorage>, IReadOnlyList<IComponentDataStorage>>>
        Factories = [];

    internal void RegisterFactory<TComponent>()
        where TComponent : IComponentData
    {
        Factories.TryAdd(
            typeof(TComponent),
            storages => storages.Cast<IComponentDataStorage<TComponent>>().ToArray());
    }

    private static readonly MethodInfo TypelessRegisterFactory = typeof(StorageArrayFactory)
        .GetMethod(nameof(RegisterFactory), BindingFlags.Instance | BindingFlags.NonPublic, Array.Empty<Type>())!;

    internal void RegisterFactory(Type type)
    {
        Guard.IsComponent(type);
        TypelessRegisterFactory.MakeGenericMethod(type).Invoke(this, null);
    }
}