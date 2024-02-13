namespace Deepslate.Ecs.SourceGenerator;

[AttributeUsage(AttributeTargets.Field)]
public abstract class QueryAttribute : Attribute;

public sealed class WithFilterAttribute(string predicate) : QueryAttribute
{
    public string Predicate { get; } = predicate;
}

public sealed class RequireInstantCommandAttribute : QueryAttribute;

public sealed class AsGenericQueryAttribute : QueryAttribute;