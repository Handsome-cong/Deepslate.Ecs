namespace Deepslate.Ecs.GenericWrapper;

#nullable enable

internal ref struct QueryEnumeratorData
{
    private readonly Archetype[] _archetypes;
    internal Span<Entity> CurrentEntitySpan = Span<Entity>.Empty;



    private int _currentArchetypeIndex = -1;
    private int _currentIndex = -1;
        public int CurrentIndex => _currentIndex;
    public Archetype CurrentArchetype { get; private set; } = default!;

    internal QueryEnumeratorData(IEnumerable<Archetype> archetypes)
    {
        _archetypes = archetypes.ToArray();
    }

    public bool MoveNext()
    {
        _currentIndex++;
        while (_currentArchetypeIndex < _archetypes.Length && _currentIndex >= CurrentArchetype.Count)
        {
            _currentArchetypeIndex++;
            _currentIndex = 0;
        }

        var success = _currentArchetypeIndex < _archetypes.Length;
        if (success)
        {
            CurrentArchetype = _archetypes[_currentArchetypeIndex];
            CurrentEntitySpan = CurrentArchetype.Entities.AsSpan();

        }

        return success;
    }

    public void Reset()
    {

        
        _currentArchetypeIndex = -1;
        _currentIndex = -1;
    }
}

internal ref struct QueryEnumeratorData<TComponent1>
    where TComponent1 : IComponent
{
    private readonly Archetype[] _archetypes;
    internal Span<Entity> CurrentEntitySpan = Span<Entity>.Empty;

    internal Span<TComponent1> CurrentComponent1Span = Span<TComponent1>.Empty;

    private int _currentArchetypeIndex = -1;
    private int _currentIndex = -1;
        public int CurrentIndex => _currentIndex;
    public Archetype CurrentArchetype { get; private set; } = default!;

    internal QueryEnumeratorData(IEnumerable<Archetype> archetypes)
    {
        _archetypes = archetypes.ToArray();
    }

    public bool MoveNext()
    {
        _currentIndex++;
        while (_currentArchetypeIndex < _archetypes.Length && _currentIndex >= CurrentArchetype.Count)
        {
            _currentArchetypeIndex++;
            _currentIndex = 0;
        }

        var success = _currentArchetypeIndex < _archetypes.Length;
        if (success)
        {
            CurrentArchetype = _archetypes[_currentArchetypeIndex];
            CurrentEntitySpan = CurrentArchetype.Entities.AsSpan();
            CurrentComponent1Span = CurrentArchetype.GetStorage<TComponent1>().AsSpan();
        }

        return success;
    }

    public void Reset()
    {
        CurrentComponent1Span = Span<TComponent1>.Empty;
        
        _currentArchetypeIndex = -1;
        _currentIndex = -1;
    }
}

internal ref struct QueryEnumeratorData<TComponent1, TComponent2>
    where TComponent1 : IComponent
    where TComponent2 : IComponent
{
    private readonly Archetype[] _archetypes;
    internal Span<Entity> CurrentEntitySpan = Span<Entity>.Empty;

    internal Span<TComponent1> CurrentComponent1Span = Span<TComponent1>.Empty;
    internal Span<TComponent2> CurrentComponent2Span = Span<TComponent2>.Empty;

    private int _currentArchetypeIndex = -1;
    private int _currentIndex = -1;
        public int CurrentIndex => _currentIndex;
    public Archetype CurrentArchetype { get; private set; } = default!;

    internal QueryEnumeratorData(IEnumerable<Archetype> archetypes)
    {
        _archetypes = archetypes.ToArray();
    }

    public bool MoveNext()
    {
        _currentIndex++;
        while (_currentArchetypeIndex < _archetypes.Length && _currentIndex >= CurrentArchetype.Count)
        {
            _currentArchetypeIndex++;
            _currentIndex = 0;
        }

        var success = _currentArchetypeIndex < _archetypes.Length;
        if (success)
        {
            CurrentArchetype = _archetypes[_currentArchetypeIndex];
            CurrentEntitySpan = CurrentArchetype.Entities.AsSpan();
            CurrentComponent1Span = CurrentArchetype.GetStorage<TComponent1>().AsSpan();
            CurrentComponent2Span = CurrentArchetype.GetStorage<TComponent2>().AsSpan();
        }

        return success;
    }

    public void Reset()
    {
        CurrentComponent1Span = Span<TComponent1>.Empty;
        CurrentComponent2Span = Span<TComponent2>.Empty;
        
        _currentArchetypeIndex = -1;
        _currentIndex = -1;
    }
}

internal ref struct QueryEnumeratorData<TComponent1, TComponent2, TComponent3>
    where TComponent1 : IComponent
    where TComponent2 : IComponent
    where TComponent3 : IComponent
{
    private readonly Archetype[] _archetypes;
    internal Span<Entity> CurrentEntitySpan = Span<Entity>.Empty;

    internal Span<TComponent1> CurrentComponent1Span = Span<TComponent1>.Empty;
    internal Span<TComponent2> CurrentComponent2Span = Span<TComponent2>.Empty;
    internal Span<TComponent3> CurrentComponent3Span = Span<TComponent3>.Empty;

    private int _currentArchetypeIndex = -1;
    private int _currentIndex = -1;
        public int CurrentIndex => _currentIndex;
    public Archetype CurrentArchetype { get; private set; } = default!;

    internal QueryEnumeratorData(IEnumerable<Archetype> archetypes)
    {
        _archetypes = archetypes.ToArray();
    }

    public bool MoveNext()
    {
        _currentIndex++;
        while (_currentArchetypeIndex < _archetypes.Length && _currentIndex >= CurrentArchetype.Count)
        {
            _currentArchetypeIndex++;
            _currentIndex = 0;
        }

        var success = _currentArchetypeIndex < _archetypes.Length;
        if (success)
        {
            CurrentArchetype = _archetypes[_currentArchetypeIndex];
            CurrentEntitySpan = CurrentArchetype.Entities.AsSpan();
            CurrentComponent1Span = CurrentArchetype.GetStorage<TComponent1>().AsSpan();
            CurrentComponent2Span = CurrentArchetype.GetStorage<TComponent2>().AsSpan();
            CurrentComponent3Span = CurrentArchetype.GetStorage<TComponent3>().AsSpan();
        }

        return success;
    }

    public void Reset()
    {
        CurrentComponent1Span = Span<TComponent1>.Empty;
        CurrentComponent2Span = Span<TComponent2>.Empty;
        CurrentComponent3Span = Span<TComponent3>.Empty;
        
        _currentArchetypeIndex = -1;
        _currentIndex = -1;
    }
}

internal ref struct QueryEnumeratorData<TComponent1, TComponent2, TComponent3, TComponent4>
    where TComponent1 : IComponent
    where TComponent2 : IComponent
    where TComponent3 : IComponent
    where TComponent4 : IComponent
{
    private readonly Archetype[] _archetypes;
    internal Span<Entity> CurrentEntitySpan = Span<Entity>.Empty;

    internal Span<TComponent1> CurrentComponent1Span = Span<TComponent1>.Empty;
    internal Span<TComponent2> CurrentComponent2Span = Span<TComponent2>.Empty;
    internal Span<TComponent3> CurrentComponent3Span = Span<TComponent3>.Empty;
    internal Span<TComponent4> CurrentComponent4Span = Span<TComponent4>.Empty;

    private int _currentArchetypeIndex = -1;
    private int _currentIndex = -1;
        public int CurrentIndex => _currentIndex;
    public Archetype CurrentArchetype { get; private set; } = default!;

    internal QueryEnumeratorData(IEnumerable<Archetype> archetypes)
    {
        _archetypes = archetypes.ToArray();
    }

    public bool MoveNext()
    {
        _currentIndex++;
        while (_currentArchetypeIndex < _archetypes.Length && _currentIndex >= CurrentArchetype.Count)
        {
            _currentArchetypeIndex++;
            _currentIndex = 0;
        }

        var success = _currentArchetypeIndex < _archetypes.Length;
        if (success)
        {
            CurrentArchetype = _archetypes[_currentArchetypeIndex];
            CurrentEntitySpan = CurrentArchetype.Entities.AsSpan();
            CurrentComponent1Span = CurrentArchetype.GetStorage<TComponent1>().AsSpan();
            CurrentComponent2Span = CurrentArchetype.GetStorage<TComponent2>().AsSpan();
            CurrentComponent3Span = CurrentArchetype.GetStorage<TComponent3>().AsSpan();
            CurrentComponent4Span = CurrentArchetype.GetStorage<TComponent4>().AsSpan();
        }

        return success;
    }

    public void Reset()
    {
        CurrentComponent1Span = Span<TComponent1>.Empty;
        CurrentComponent2Span = Span<TComponent2>.Empty;
        CurrentComponent3Span = Span<TComponent3>.Empty;
        CurrentComponent4Span = Span<TComponent4>.Empty;
        
        _currentArchetypeIndex = -1;
        _currentIndex = -1;
    }
}

