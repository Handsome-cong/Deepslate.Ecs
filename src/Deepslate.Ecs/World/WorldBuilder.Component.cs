using System.Runtime.InteropServices;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

public sealed partial class WorldBuilder
{
    private readonly List<Archetype> _archetypes = [];

    private readonly HashSet<Type> _managedComponentTypes = [];
    private readonly HashSet<Type> _unmanagedComponentTypes = [];
    private readonly HashSet<Type> _componentTypes = [];
    private readonly Dictionary<Type, List<int>> _componentTypeToArchetypeIds = [];

    private readonly StorageArrayFactory _storageArrayFactory = new();
    
    internal readonly ComponentStorageFactory ComponentStorageFactory = new();

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

        _managedComponentTypes.Add(componentType);
        RegisterManagedFactories(componentType);
        return this;
    }

    public WorldBuilder WithManagedComponent<TComponent>()
        where TComponent : IComponent
    {
        var componentType = typeof(TComponent);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

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

        _unmanagedComponentTypes.Add(componentType);
        RegisterUnmanagedFactories(componentType);
        return this;
    }

    public WorldBuilder WithUnmanagedComponent<TComponent>()
        where TComponent : unmanaged, IComponent
    {
        var componentType = typeof(TComponent);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

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
        where TComponent : IComponent
    {
        var componentType = typeof(TComponent);
        if (!_componentTypes.Add(componentType))
        {
            return this;
        }

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
        foreach (var existentArchetype in _archetypes)
        {
            if (existentArchetype.ComponentTypesHashCode != archetype.ComponentTypesHashCode ||
                existentArchetype.ComponentTypes.Count != archetype.ComponentTypes.Count)
            {
                continue;
            }

            var sameTypes = true;
            for (var i = 0; i < archetype.ComponentTypes.Count; i++)
            {
                sameTypes &= existentArchetype.ComponentTypes[i] == archetype.ComponentTypes[i];
            }

            if (!sameTypes)
            {
                continue;
            }

            registeredArchetype = existentArchetype;
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
        where TComponent : IComponent
    {
        _storageArrayFactory.RegisterFactory<TComponent>();
        ComponentStorageFactory.RegisterManagedFactory<TComponent>();
    }

    private void RegisterUnmanagedFactories<TComponent>()
        where TComponent : unmanaged, IComponent
    {
        _storageArrayFactory.RegisterFactory<TComponent>();
        ComponentStorageFactory.RegisterUnmanagedFactory<TComponent>();
    }

    private void RegisterManagedFactories(Type componentType)
    {
        _storageArrayFactory.RegisterFactory(componentType);
        ComponentStorageFactory.RegisterManagedFactory(componentType);
    }

    private void RegisterUnmanagedFactories(Type componentType)
    {
        _storageArrayFactory.RegisterFactory(componentType);
        ComponentStorageFactory.RegisterUnmanagedFactory(componentType);
    }
}