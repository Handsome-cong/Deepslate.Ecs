using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deepslate.Ecs;

/// <summary>
/// <para>
/// An simple 64-bit integer that represents an entity.
/// </para>
/// <para>
/// You can store an <see cref="Entity"/> freely, and in almost all cases you don't need to worry about collisions.
/// </para>
/// <para>
/// Due to the implementation of this,
/// the number of <see cref="Archetype"/> per <see cref="World"/> is limited to 2^16 (65536),
/// and the number of <see cref="Entity"/> per <see cref="Archetype"/> is limited to 2^32 (4294967296).
/// </para>
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = sizeof(ulong))]
public readonly struct Entity : IEquatable<Entity>, IEqualityOperators<Entity, Entity, bool>
{
    // Technically, 32-bit, which means 4 billion entities, is enough for most applications.
    // However, 64-bit is much more easier to process, especially there are three parts in an entity.

    [FieldOffset(0)] internal readonly ulong Value;
    [FieldOffset(0)] internal readonly uint Id;
    [FieldOffset(sizeof(uint))] internal readonly ushort ArchetypeId;
    [FieldOffset(sizeof(uint) + sizeof(ushort))] internal readonly ushort Version;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity(ulong value) => Value = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity(uint id, ushort archetypeId, ushort version)
    {
        Id = id;
        ArchetypeId = archetypeId;
        Version = version;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity BumpVersion() => new(Id, ArchetypeId, (ushort)(Version + 1));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator ulong(Entity entity) => entity.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Entity(ulong value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Entity left, Entity right) => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Entity left, Entity right) => !left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is Entity other && Equals(other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Entity other) => Value == other.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => Value.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => Value.ToString();
}

internal record struct ComponentizedEntity(Entity Entity) : IComponentData
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Entity(ComponentizedEntity componentizedEntity) => componentizedEntity.Entity;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ComponentizedEntity(Entity entity) => new(entity);
}
