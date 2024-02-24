using System.Diagnostics.CodeAnalysis;

namespace Deepslate.Ecs.Benchmark.Oop;

public sealed class GameEntity
{
    private readonly List<Component> _components = [];

    internal uint Id { get; }
    public bool Alive { get; internal set; } = false;

    internal GameEntity(uint id)
    {
        Id = id;
    }

    internal void Update()
    {
        foreach (var component in _components)
        {
            component.Update();
        }
    }

    public TComponent AddComponent<TComponent>()
        where TComponent : Component, new()
    {
        var component = new TComponent
        {
            Entity = this
        };
        _components.Add(component);
        component.Startup();
        return component;
    }

    public void RemoveComponent<TComponent>()
        where TComponent : Component
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i] is not TComponent)
            {
                continue;
            }

            _components.RemoveAt(i);
            return;
        }
    }

    public bool TryGetComponent<TComponent>([MaybeNullWhen(false)] out TComponent component)
        where TComponent : Component
    {
        for (var i = 0; i < _components.Count; i++)
        {
            if (_components[i] is not TComponent)
            {
                continue;
            }

            component = (TComponent)_components[i];
            return true;
        }

        component = null;
        return false;
    }

    public void RemoveAllComponents()
    {
        _components.Clear();
    }
}