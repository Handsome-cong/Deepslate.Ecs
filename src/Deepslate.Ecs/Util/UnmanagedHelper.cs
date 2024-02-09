using System.Reflection;
using System.Runtime.CompilerServices;

namespace Deepslate.Ecs.Util;

internal static class UnmanagedHelper
{
    public static bool IsUnmanaged(Type type)
    {
        var result = typeof(RuntimeHelpers)
            .GetMethod("IsReferenceOrContainsReferences", BindingFlags.Static | BindingFlags.Public)
            ?.MakeGenericMethod(type).Invoke(null, null);
        return !(bool) result!;
    }
    
    public static bool IsUnmanaged<T>()
    {
        return !RuntimeHelpers.IsReferenceOrContainsReferences<T>();
    }
}