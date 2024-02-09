using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

public readonly struct EntityComponentAccessor
{
    public Entity Entity { get; }
    public Archetype Archetype { get; }
    public bool Alive => Archetype.ContainsEntity(Entity);

    internal EntityComponentAccessor(Entity entity, Archetype archetype)
    {
        Entity = entity;
        Archetype = archetype;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref TComponent GetComponent<TComponent>()
        where TComponent : IComponentData => ref Archetype.GetComponent<TComponent>(Entity);
}