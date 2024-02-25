namespace Deepslate.Ecs.GenericWrapper;

public ref struct ArchetypeEnumerator<TComponent>
    where TComponent : IComponent
{
    private ReadOnlySpan<Archetype>.Enumerator _enumerator;

    public StorageSpan Current => new(_enumerator.Current);
    
    internal ArchetypeEnumerator(IReadOnlyList<Archetype> archetypes)
    {
        var array = (Archetype[])archetypes;
        ReadOnlySpan<Archetype> span = array;
        _enumerator = span.GetEnumerator();
    }
    
    public bool MoveNext() => _enumerator.MoveNext();
    
    public ref struct StorageSpan
    {
        public Archetype Archetype { get; private set; } = Archetype.Empty;

        public int Count { get; private set; } = 0;
        public ReadOnlySpan<Entity> Entities { get; private set; } = ReadOnlySpan<Entity>.Empty;
        public ReadOnlySpan<TComponent> ReadOnlyComponents { get; private set; } = ReadOnlySpan<TComponent>.Empty;

        internal StorageSpan(Archetype archetype)
        {
            Archetype = archetype;
            Update();
        }

        public void Update()
        {
            Count = Archetype.Count;
            Entities = Archetype.Entities;
            ReadOnlyComponents = Archetype.GetComponents<TComponent>(Range.All);
        }
    }
}
