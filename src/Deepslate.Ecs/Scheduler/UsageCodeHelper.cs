namespace Deepslate.Ecs;

internal static class UsageCodeHelper
{
    public static void FillUsageCode<T>(
        Span<UsageCode> usageCodes,
        IEnumerable<T> source,
        IReadOnlyDictionary<T, int> sourceOffset)
    {
        foreach (var componentType in source)
        {
            var componentTypeId = sourceOffset[componentType];
            ref var componentUsageCode = ref usageCodes[componentTypeId / UsageCode.SizeOfBits];
            componentUsageCode = componentUsageCode.WithBitOffset(componentTypeId % UsageCode.SizeOfBits);
        }
    }
    
    public static int GetUsageCodeCount(int count)
    {
        return (count - 1) / UsageCode.SizeOfBits + 1;
    }
}