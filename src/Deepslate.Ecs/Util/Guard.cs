using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Deepslate.Ecs.Util;

internal static class Guard
{
    [DebuggerStepThrough]
    public static void IsComponent(
        Type type,
        [CallerArgumentExpression(nameof(type))]
        string argument = "Unknown")
    {
        if (!typeof(IComponentData).IsAssignableFrom(type))
        {
            throw new ArgumentException("Type must implement IComponentData", argument);
        }
    }

    [DebuggerStepThrough]
    public static void IsUnmanaged(
        Type type,
        [CallerArgumentExpression(nameof(type))]
        string argument = "Unknown")
    {
        if (!UnmanagedHelper.IsUnmanaged(type))
        {
            throw new ArgumentException("Type must be unmanaged", argument);
        }
    }

    public static void ArchetypeIsNotEmptyOrNull(
        Archetype? archetype,
        [CallerArgumentExpression(nameof(archetype))]
        string argument = "Unknown")
    {
        if (archetype is null || archetype == Archetype.Empty)
        {
            throw new ArgumentException("Archetype must not be null or empty", argument);
        }
    }
}