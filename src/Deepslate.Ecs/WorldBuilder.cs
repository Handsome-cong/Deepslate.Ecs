namespace Deepslate.Ecs;

public sealed class WorldBuilder
{
    private readonly Dictionary<int, ArchetypeInfo> _archetypeInfos = new();
    private readonly List<Type> _managedComponentTypes = [];
    private readonly List<Type> _unmanagedComponentTypes = [];
    private readonly HashSet<Type> _componentTypes = [];

    public IEnumerable<ArchetypeInfo> ArchetypeInfos => _archetypeInfos.Values;
    public IEnumerable<Type> ComponentTypes => _componentTypes;
    public IEnumerable<Type> ManagedComponentTypes => _managedComponentTypes;
    public IEnumerable<Type> UnmanagedComponentTypes => _unmanagedComponentTypes;

    public WorldBuilder WithArchetype(IEnumerable<Type> info, out ArchetypeInfo archetypeInfo)
    {
        archetypeInfo = new ArchetypeInfo(info);
        _archetypeInfos.Add(archetypeInfo.GetHashCode(), archetypeInfo);

        return this;
    }

    public WorldBuilder WithManagedComponent(Type type)
    {
        Guard.IsComponent(type);

        if (_componentTypes.Add(type))
        {
            _managedComponentTypes.Add(type);
        }

        return this;
    }

    public WorldBuilder WithUnmanagedComponent(Type type)
    {
        if (!typeof(IComponent).IsAssignableFrom(type))
        {
            throw new ArgumentException("Type must implement IComponent", nameof(type));
        }

        if (_componentTypes.Add(type))
        {
            _unmanagedComponentTypes.Add(type);
        }

        return this;
    }
}

/// <summary>
/// This class exists for keeping <see cref="WorldBuilder"/> clean.
/// </summary>
public static class WorldBuilderExtensions
{
    public static WorldBuilder WithArchetype(this WorldBuilder self, IEnumerable<Type> info) =>
        self.WithArchetype(info, out _);

    public static WorldBuilder WithManagedComponent<T>(this WorldBuilder self)
        where T : IComponent => self.WithManagedComponent(typeof(T));

    public static WorldBuilder WithUnmanagedComponent<T>(this WorldBuilder self)
        where T : unmanaged, IComponent => self.WithUnmanagedComponent(typeof(T));

    public static bool VerifyAllRequiredComponentTypeRegistered(this WorldBuilder self) =>
        self.ArchetypeInfos
            .SelectMany(archetypeInfo => archetypeInfo.ComponentTypes)
            .All(componentType => self.ComponentTypes.Contains(componentType));
}
