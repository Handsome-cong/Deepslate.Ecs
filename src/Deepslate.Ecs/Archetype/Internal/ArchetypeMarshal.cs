namespace Deepslate.Ecs.Internal;

public static class ArchetypeMarshal
{
    /// <summary>
    /// This is mainly used by the source generator.
    /// </summary>
    public static IComponentDataStorage<TComponent> GetStorage<TComponent>(Archetype archetype)
        where TComponent : IComponentData
    {
        if (!archetype.ComponentStorageDictionary.TryGetValue(typeof(TComponent), out var storage))
        {
            throw new ArgumentOutOfRangeException(nameof(TComponent), "Component does not exist in this archetype.");
        }

        return (IComponentDataStorage<TComponent>)storage;
    }
}