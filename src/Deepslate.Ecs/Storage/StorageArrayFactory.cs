using System.Reflection;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

/// <summary>
/// This is mainly used to convert <see cref="IComponentStorage"/>[]
/// to <see cref="IComponentStorage{TComponent}"/>[].
/// </summary>
internal sealed class StorageArrayFactory
{
    internal readonly
        Dictionary<Type, Func<IEnumerable<IComponentStorage>, IReadOnlyList<IComponentStorage>>>
        Factories = [];

    internal void RegisterFactory<TComponent>()
        where TComponent : IComponent
    {
        Factories.TryAdd(
            typeof(TComponent),
            storages => storages.Cast<IComponentStorage<TComponent>>().ToArray());
    }

    private static readonly MethodInfo TypelessRegisterFactory = typeof(StorageArrayFactory)
        .GetMethod(nameof(RegisterFactory), BindingFlags.Instance | BindingFlags.NonPublic, Array.Empty<Type>())!;

    internal void RegisterFactory(Type type)
    {
        Guard.IsComponent(type);
        TypelessRegisterFactory.MakeGenericMethod(type).Invoke(this, null);
    }
}