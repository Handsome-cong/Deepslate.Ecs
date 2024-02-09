using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

#pragma warning disable CS0414 // Field is assigned but its value is never used
namespace Deepslate.Ecs;

/// <summary>
/// 512-bits
/// </summary>
internal struct UsageCode
{
    public const int SizeOfBits = 512;
    
    public static readonly UsageCode None = new() { _data = Vector512<ulong>.Zero };
    public static readonly UsageCode All = new() { _data = Vector512<ulong>.AllBitsSet };

    private static readonly Vector512<ulong>[] SingleOne;

    private Vector512<ulong> _data;

    static UsageCode()
    {
        SingleOne = new Vector512<ulong>[512];
        for (var i = 0; i < 512; i++)
        {
            const int elementSize = sizeof(ulong) * 8;
            ref var code = ref SingleOne[i];
            code = None._data.WithElement(i / elementSize, 1ul << (i % elementSize));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool ConflictWith(ref readonly UsageCode other)
    {
        var result = Vector512.BitwiseAnd(_data, other._data);
        return !Vector512.EqualsAll(result, Vector512<ulong>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UsageCode WithFlagOffset(int flagOffset)
    {
        return new UsageCode { _data = Vector512.BitwiseOr(_data, SingleOne[flagOffset]) };
    }
}

internal readonly ref struct UsageCodeBundle(
    ReadOnlySpan<UsageCode> data,
    int archetypeCodeCountPerQuery,
    int componentTypeCodeCountPerQuery)
{
    private readonly ReadOnlySpan<UsageCode> _data = data;
    private int UsageCountPerQuery => archetypeCodeCountPerQuery + componentTypeCodeCountPerQuery * 2;
    private int QueryCount => _data.Length / UsageCountPerQuery;

    public bool ConflictWith(UsageCodeBundle other)
    {
        var usageCountPerQuery = UsageCountPerQuery;
        for (var i = 0; i < QueryCount; i++)
        {
            var selfQuery = _data.Slice(i * usageCountPerQuery, usageCountPerQuery);
            for (var j = 0; j < other.QueryCount; j++)
            {
                var otherQuery = other._data.Slice(i * usageCountPerQuery, usageCountPerQuery);
                if (QueryConflict(selfQuery, otherQuery))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool QueryConflict(ReadOnlySpan<UsageCode> left, ReadOnlySpan<UsageCode> right)
    {
        var leftArchetypeCodeSpan = left[..archetypeCodeCountPerQuery];
        var rightArchetypeCodeSpan = right[..archetypeCodeCountPerQuery];
        if (!SpanConflict(leftArchetypeCodeSpan, rightArchetypeCodeSpan))
        {
            return false;
        }

        var writableRange = archetypeCodeCountPerQuery..
            (archetypeCodeCountPerQuery + componentTypeCodeCountPerQuery);
        var readableRange = (archetypeCodeCountPerQuery + componentTypeCodeCountPerQuery)..
            (archetypeCodeCountPerQuery + componentTypeCodeCountPerQuery * 2);
        
        var leftWritableSpan = left[writableRange];
        var rightWritableSpan = right[writableRange];
        var leftReadableSpan = left[readableRange];
        var rightReadableSpan = right[readableRange];
        return SpanConflict(leftWritableSpan, rightReadableSpan) || SpanConflict(leftReadableSpan, rightWritableSpan);
    }

    private static bool SpanConflict(ReadOnlySpan<UsageCode> left, ReadOnlySpan<UsageCode> right)
    {
        for (var i = 0; i < left.Length; i++)
        {
            if (left[i].ConflictWith(in right[i]))
            {
                return true;
            }
        }

        return false;
    }
}