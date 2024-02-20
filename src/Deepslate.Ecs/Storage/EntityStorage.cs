namespace Deepslate.Ecs;

internal struct EntityStorage()
{
    private const int SizeOfPage = IComponentStorage.SizeOfPage;
    public const int NoIndex = -1;
    
    // A sparse set was used to store the entities in the archetype.
    // For more information about sparse set, see:
    // https://skypjack.github.io/2020-08-02-ecs-baf-part-9/

    private Entity[] _entities = Array.Empty<Entity>();
    private int[]?[] _indicesPages = Array.Empty<int[]>();
    
    private readonly Queue<Entity> _removedEntities = new();
    private uint _maxEntityId;

    /// <summary>
    /// The number of entities in this storage.
    /// </summary>
    public int Count { get; private set; }
    
    /// <summary>
    /// Get a <see cref="Span{T}"/> of all entities in this storage.
    /// </summary>
    public Span<Entity> AsSpan() => _entities.AsSpan(0, Count);

    /// <summary>
    /// Get the index of entity in the storage.
    /// The index is also the index of components that associated with the entity.
    /// </summary>
    /// <param name="entity">
    /// The entity whose index you want to get.
    /// </param>
    /// <returns>
    /// The index of entity in the storage, or <see cref="NoIndex"/> if the entity does not exist in this storage.
    /// </returns>
    public int IndexOf(Entity entity)
    {
        var pageIndex = (int)entity.Id / SizeOfPage;
        var indexInPage = (int)entity.Id % SizeOfPage;

        if (_indicesPages[pageIndex] is not { } page)
        {
            return NoIndex;
        }

        var dataIndex = page[indexInPage];
        return dataIndex;
    }
    
    public Entity AddEntity(ushort archetypeId)
    {
        if (_removedEntities.TryDequeue(out var entity))
        {
            entity = entity.BumpVersion();
        }
        else
        {
            entity = new Entity(_maxEntityId++, archetypeId, 0);
        }
        
        var pageIndex = (int)entity.Id / SizeOfPage;
        var indexInPage = (int)entity.Id % SizeOfPage;

        var page = EnsurePage(pageIndex);
        EnsureDataCapacity(Count);
        _entities[Count] = entity;
        page[indexInPage] = Count++;
        return entity;
    }

    public Entity[] AddEntities(ushort archetypeId, int count)
    {
        EnsureDataCapacity(Count + count);
        var entities = new Entity[count];
        for (var i = 0; i < count; i++)
        {
            entities[i] = AddEntity(archetypeId);
        }
        return entities;
    }

    public bool RemoveEntity(Entity entity, out int dataIndex)
    {
        dataIndex = NoIndex;
        var pageIndex = (int)entity.Id / SizeOfPage;
        var indexInPage = (int)entity.Id % SizeOfPage;

        if (_indicesPages[pageIndex] is not { } page)
        {
            return false;
        }

        dataIndex = page[indexInPage];
        if (dataIndex == NoIndex)
        {
            return false;
        }

        Count--;
        if (dataIndex < Count)
        {
            var tail = _entities[Count];
            _entities[dataIndex] = tail;
            
            var tailPageIndex = (int)tail.Id / SizeOfPage;
            var tailIndexInPage = (int)tail.Id % SizeOfPage;
            _indicesPages[tailPageIndex]![tailIndexInPage] = dataIndex;
        }
        page[indexInPage] = NoIndex;
        return true;
    }

    public int[] RemoveEntities(Span<Entity> entities)
    {
        var dataIndices = new int[entities.Length];
        Array.Fill(dataIndices, NoIndex);
        for(var i = 0; i<entities.Length;i++)
        {
            var entity = entities[i];
            var pageIndex = (int)entity.Id / SizeOfPage;
            var indexInPage = (int)entity.Id % SizeOfPage;

            if (_indicesPages[pageIndex] is not { } page)
            {
                continue;
            }

            var dataIndex = page[indexInPage];
            if (dataIndex == NoIndex)
            {
                continue;
            }

            dataIndices[i] = dataIndex;
        }
        
        for(var i = 0; i<entities.Length;i++)
        {
            var entity = entities[i];
            if (!RemoveEntity(entity, out _))
            {
                dataIndices[i] = NoIndex;
            }
        }
        return dataIndices;
    }

    private int[] EnsurePage(int pageIndex)
    {
        if (pageIndex >= _indicesPages.Length)
        {
            var newPages = new int[pageIndex + 1][];
            Array.Copy(_indicesPages, newPages, _indicesPages.Length);
            _indicesPages = newPages;
        }

        ref var page = ref _indicesPages[pageIndex];
        if (page is null)
        {
            page = new int[SizeOfPage];
            Array.Fill(page, NoIndex);
        }

        return page;
    }

    private void EnsureDataCapacity(int dataIndex)
    {
        if (dataIndex < _entities.Length)
        {
            return;
        }

        var newLength = (dataIndex / SizeOfPage + 1) * SizeOfPage;
        var newData = new Entity[newLength];
        Array.Copy(_entities, newData, Count);
        _entities = newData;
    }
}
