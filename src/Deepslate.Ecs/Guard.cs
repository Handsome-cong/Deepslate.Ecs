using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Deepslate.Ecs;

internal static class Guard
{
    [DebuggerStepThrough]
    public static void IsComponent(
        Type type,
        [CallerArgumentExpression(nameof(type))] string argument = "Unknown"
    )
    {
        if (!typeof(IComponent).IsAssignableFrom(type))
        {
            throw new ArgumentException("Type must implement IComponent", argument);
        }
    }

    [DebuggerStepThrough]
    public static void IsComponent<T>() => IsComponent(typeof(T), $"Generic Type: {typeof(T)}");
}
