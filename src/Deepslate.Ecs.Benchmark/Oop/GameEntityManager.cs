namespace Deepslate.Ecs.Benchmark.Oop;

public sealed class GameEntityManager
{
    private readonly List<GameEntity> _entities = [];
    
    private readonly Queue<GameEntity> _recycledEntities = [];

    public void Update()
    {
        foreach (var entity in _entities)
        {
            entity.Update();
        }
    }

    public GameEntity CreateEntity()
    {
        if (_recycledEntities.TryDequeue(out var entity))
        {
            entity.Alive = true;
            return entity;
        }

        entity = new GameEntity((uint)_entities.Count) { Alive = true };
        _entities.Add(entity);
        return entity;
    }

    public void DestroyEntity(GameEntity entity)
    {
        entity.Alive = false;
        _recycledEntities.Enqueue(entity);
    }
}