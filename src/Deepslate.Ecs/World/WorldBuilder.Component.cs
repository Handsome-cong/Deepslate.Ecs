using System.Reflection;
using System.Runtime.InteropServices;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

public sealed partial class WorldBuilder
{
    private readonly List<Archetype> _archetypes = [];

    private readonly HashSet<Type> _managedComponentTypes = [];
    private readonly HashSet<Type> _unmanagedComponentTypes = [];
    private readonly HashSet<Type> _componentTypes = [];
    private readonly Dictionary<Type, int> _componentTypeIds = [];
    private readonly Dictionary<Type, List<int>> _componentTypeToArchetypeIds = [];

    internal readonly ComponentStorageFactory ComponentStorageFactory = new();
    internal readonly StorageArrayFactory StorageArrayFactory = new();

    internal IReadOnlyDictionary<Type, List<int>> ComponentTypeToArchetypeIds => _componentTypeToArchetypeIds;
    internal IReadOnlyDictionary<Type, int> ComponentTypeIds => _componentTypeIds;

    public IReadOnlySet<Type> ComponentTypes => _componentTypes;
    public IReadOnlySet<Type> ManagedComponentTypes => _managedComponentTypes;
    public IReadOnlySet<Type> UnmanagedComponentTypes => _unmanagedComponentTypes;
    public IReadOnlyList<Archetype> Archetypes => _archetypes;


    public WorldBuilder WithManagedComponent(Type componentType)
    {
        Guard.IsComponent(componentType);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

        _componentTypeIds.Add(componentType, _componentTypeIds.Count);
        _managedComponentTypes.Add(componentType);
        RegisterManagedFactories(componentType);
        return this;
    }

    public WorldBuilder WithManagedComponent<TComponent>()
        where TComponent : IComponentData
    {
        var componentType = typeof(TComponent);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

        _componentTypeIds.Add(componentType, _componentTypeIds.Count);
        _managedComponentTypes.Add(componentType);
        RegisterManagedFactories<TComponent>();
        return this;
    }

    public WorldBuilder WithUnmanagedComponent(Type componentType)
    {
        Guard.IsComponent(componentType);
        Guard.IsUnmanaged(componentType);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

        _componentTypeIds.Add(componentType, _componentTypeIds.Count);
        _unmanagedComponentTypes.Add(componentType);
        RegisterUnmanagedFactories(componentType);
        return this;
    }

    public WorldBuilder WithUnmanagedComponent<TComponent>()
        where TComponent : unmanaged, IComponentData
    {
        var componentType = typeof(TComponent);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

        _componentTypeIds.Add(componentType, _componentTypeIds.Count);
        _unmanagedComponentTypes.Add(componentType);
        RegisterUnmanagedFactories<TComponent>();
        return this;
    }

    public WorldBuilder WithUnmanagedComponentIfPossible(Type componentType)
    {
        Guard.IsComponent(componentType);

        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

        _componentTypeIds.Add(componentType, _componentTypeIds.Count);
        if (UnmanagedHelper.IsUnmanaged(componentType))
        {
            _unmanagedComponentTypes.Add(componentType);
            RegisterUnmanagedFactories(componentType);
        }
        else
        {
            _managedComponentTypes.Add(componentType);
            RegisterManagedFactories(componentType);
        }

        return this;
    }

    public WorldBuilder WithUnmanagedComponentIfPossible<TComponent>()
        where TComponent : IComponentData
    {
        var componentType = typeof(TComponent);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

        _componentTypeIds.Add(componentType, _componentTypeIds.Count);
        if (UnmanagedHelper.IsUnmanaged<TComponent>())
        {
            _unmanagedComponentTypes.Add(componentType);
            RegisterUnmanagedFactories(componentType);
        }
        else
        {
            _managedComponentTypes.Add(componentType);
            RegisterManagedFactories<TComponent>();
        }

        return this;
    }

    public ArchetypeBuilder WithArchetype() => new(this);
    internal ushort NextArchetypeId => (ushort)_archetypes.Count;

    internal bool TryRegisterArchetype(Archetype archetype, out Archetype registeredArchetype)
    {
        foreach (var existingArchetype in _archetypes)
        {
            if (existingArchetype.ComponentTypesHashCode != archetype.ComponentTypesHashCode ||
                existingArchetype.ComponentTypes.Count != archetype.ComponentTypes.Count)
            {
                continue;
            }

            var sameTypes = true;
            for (var i = 0; i < archetype.ComponentTypes.Count; i++)
            {
                sameTypes &= existingArchetype.ComponentTypes[i] == archetype.ComponentTypes[i];
            }

            if (!sameTypes)
            {
                continue;
            }

            registeredArchetype = existingArchetype;
            return false;
        }

        var index = _archetypes.Count;
        _archetypes.Add(archetype);
        foreach (var componentType in archetype.ComponentTypes)
        {
            ref var indices =
                ref CollectionsMarshal.GetValueRefOrAddDefault(_componentTypeToArchetypeIds, componentType, out _);

            if (indices is null)
            {
                indices = [];
            }

            indices.Add(index);
        }

        registeredArchetype = archetype;
        return true;
    }

    private void RegisterManagedFactories<TComponent>()
        where TComponent : IComponentData
    {
        StorageArrayFactory.RegisterFactory<TComponent>();
        ComponentStorageFactory.RegisterManagedFactory<TComponent>();
    }

    private void RegisterUnmanagedFactories<TComponent>()
        where TComponent : unmanaged, IComponentData
    {
        StorageArrayFactory.RegisterFactory<TComponent>();
        ComponentStorageFactory.RegisterUnmanagedFactory<TComponent>();
    }

    private void RegisterManagedFactories(Type componentType)
    {
        StorageArrayFactory.RegisterFactory(componentType);
        ComponentStorageFactory.RegisterManagedFactory(componentType);
    }

    private void RegisterUnmanagedFactories(Type componentType)
    {
        StorageArrayFactory.RegisterFactory(componentType);
        ComponentStorageFactory.RegisterUnmanagedFactory(componentType);
    }
}