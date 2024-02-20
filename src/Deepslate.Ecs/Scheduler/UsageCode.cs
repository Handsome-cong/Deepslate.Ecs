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
    public UsageCode WithBitOffset(int flagOffset)
    {
        return new UsageCode { _data = Vector512.BitwiseOr(_data, SingleOne[flagOffset]) };
    }
}

internal readonly ref struct UsageCodeBundle(
    ReadOnlySpan<UsageCode> data,
    ReadOnlySpan<bool> instantCommandFlags,
    int allResourceCount,
    int allArchetypeCount,
    int allComponentTypeCount)
{
    // ReSharper disable once ReplaceWithPrimaryConstructorParameter
    private readonly ReadOnlySpan<UsageCode> _data = data;

    // ReSharper disable once ReplaceWithPrimaryConstructorParameter
    private readonly ReadOnlySpan<bool> _instantCommandFlags = instantCommandFlags;
    private readonly int _resourceCodeCount = UsageCodeHelper.GetUsageCodeCount(allResourceCount);
    private readonly int _archetypeUsageCodeCountPerQuery = UsageCodeHelper.GetUsageCodeCount(allArchetypeCount);
    private readonly int _componentUsageCodeCountPerQuery = UsageCodeHelper.GetUsageCodeCount(allComponentTypeCount);
    private int UsageCountPerQuery => _archetypeUsageCodeCountPerQuery + _componentUsageCodeCountPerQuery * 2;
    private int QueryCount => (_data.Length - _resourceCodeCount) / UsageCountPerQuery;
    private ReadOnlySpan<UsageCode> ResourceCodeSpan => _data[.._resourceCodeCount];
    private ReadOnlySpan<UsageCode> QueryCodeSpan => _data[_resourceCodeCount..];

    public bool ConflictWith(UsageCodeBundle other)
    {
        if (SpanConflict(ResourceCodeSpan, other.ResourceCodeSpan))
        {
            return true;
        }
        for (var i = 0; i < QueryCount; i++)
        {
            for (var j = 0; j < other.QueryCount; j++)
            {
                var left = QueryCodeSpan.Slice(i * UsageCountPerQuery, UsageCountPerQuery);
                var right = other.QueryCodeSpan.Slice(j * other.UsageCountPerQuery, other.UsageCountPerQuery);
                var checkArchetypeConflictOnly = _instantCommandFlags[i] || other._instantCommandFlags[j];
                if (QueryConflict(left, right, checkArchetypeConflictOnly))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool QueryConflict(
        ReadOnlySpan<UsageCode> left,
        ReadOnlySpan<UsageCode> right,
        bool checkArchetypeConflictOnly)
    {
        var leftArchetypeCodeSpan = left[.._archetypeUsageCodeCountPerQuery];
        var rightArchetypeCodeSpan = right[.._archetypeUsageCodeCountPerQuery];
        if (!SpanConflict(leftArchetypeCodeSpan, rightArchetypeCodeSpan))
        {
            return false;
        }
        if (checkArchetypeConflictOnly)
        {
            return true;
        }

        var writableRange = _archetypeUsageCodeCountPerQuery..
            (_archetypeUsageCodeCountPerQuery + _componentUsageCodeCountPerQuery);
        var readableRange = (_archetypeUsageCodeCountPerQuery + _componentUsageCodeCountPerQuery)..
            (_archetypeUsageCodeCountPerQuery + _componentUsageCodeCountPerQuery * 2);

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