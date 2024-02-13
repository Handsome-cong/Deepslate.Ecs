using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.Util;

namespace Deepslate.Ecs;

public sealed partial class WorldBuilder
{
    private readonly Dictionary<Type, IReactSystemExecutor> _reactAfterAlloc = [];
    private readonly Dictionary<Type, IReactSystemExecutor> _reactBeforeFree = [];
    private readonly Dictionary<Type, IReactSystemExecutor> _reactAfterCreate = [];
    private readonly Dictionary<Type, IReactSystemExecutor> _reactBeforeDestroy = [];
    private readonly Dictionary<Type, IReactSystemExecutor> _reactBeforeMove = [];
    
    internal IReadOnlyDictionary<Type, IReactSystemExecutor> ReactAfterAlloc => _reactAfterAlloc;
    internal IReadOnlyDictionary<Type, IReactSystemExecutor> ReactBeforeFree => _reactBeforeFree;
    internal IReadOnlyDictionary<Type, IReactSystemExecutor> ReactAfterCreate => _reactAfterCreate;
    internal IReadOnlyDictionary<Type, IReactSystemExecutor> ReactBeforeDestroy => _reactBeforeDestroy;
    internal IReadOnlyDictionary<Type, IReactSystemExecutor> ReactBeforeMove => _reactBeforeMove;
    
    public WorldBuilder WithReactAfterAlloc<TComponent>(IReactAfterAlloc<TComponent> react)
        where TComponent : IComponentData
    {
        _reactAfterAlloc[typeof(TComponent)] = react;
        return this;
    }
    
    public WorldBuilder WithReactBeforeFree<TComponent>(IReactBeforeFree<TComponent> react)
        where TComponent : IComponentData
    {
        _reactBeforeFree[typeof(TComponent)] = react;
        return this;
    }
    
    public WorldBuilder WithReactAfterCreate<TComponent>(IReactAfterCreate<TComponent> react)
        where TComponent : IComponentData
    {
        _reactAfterCreate[typeof(TComponent)] = react;
        return this;
    }
    
    public WorldBuilder WithReactBeforeDestroy<TComponent>(IReactBeforeDestroy<TComponent> react)
        where TComponent : IComponentData
    {
        _reactBeforeDestroy[typeof(TComponent)] = react;
        return this;
    }
    
    public WorldBuilder WithReactBeforeMove<TComponent>(IReactBeforeMove<TComponent> react)
        where TComponent : IComponentData
    {
        _reactBeforeMove[typeof(TComponent)] = react;
        return this;
    }
}