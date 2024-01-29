using System.Runtime.CompilerServices;

namespace Deepslate.Ecs.GenericWrapper;

public partial struct Writable<TWritable1>
    where TWritable1 : IComponent
{
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent
    {
        public readonly ref struct WritableArchetypeCommand
        {
            public Archetype Archetype { get; }

            internal WritableArchetypeCommand(Archetype archetype)
            {
                Archetype = archetype;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Entity Create() => Archetype.Create();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Destroy(Entity entity) => Archetype.Destroy(entity);

            /// <summary>
            /// Check if the archetype contains the entity.
            /// </summary>
            /// <param name="entity">
            /// The entity to check.
            /// </param>
            /// <returns>
            /// <see langword="true"/> if the archetype contains the entity, otherwise <see langword="false"/>.
            /// </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Contains(Entity entity) => Archetype.Contains(entity);

            /// <summary>
            /// Get the component of the entity from this <see cref="Archetype"/>.
            /// </summary>
            /// <param name="entity">
            /// The entity whose component you want to get.
            /// </param>
            /// <typeparam name="TComponent">
            /// The type of the component you want to get.
            /// </typeparam>
            /// <returns>
            /// The component reference of the entity.
            /// </returns>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when the entity or the component does not exist in this <see cref="Archetype"/>.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ref readonly TComponent GetComponent<TComponent>(Entity entity)
                where TComponent : IComponent => ref Archetype.GetComponent<TComponent>(entity);
        }
    }
}
