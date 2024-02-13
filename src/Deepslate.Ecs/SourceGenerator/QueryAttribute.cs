using System.Reflection;

namespace Deepslate.Ecs.SourceGenerator;

// Whenever these changed, check the generator and make sure the code is still valid

[AttributeUsage(AttributeTargets.Field)]
public abstract class QueryAttribute : Attribute;

[AttributeUsage(AttributeTargets.Field)]
public sealed class WithFilterAttribute(string predicate) : QueryAttribute
{
    public string Predicate { get; } = predicate;
}

[AttributeUsage(AttributeTargets.Field)]
public sealed class RequireInstantCommandAttribute : QueryAttribute;

[AttributeUsage(AttributeTargets.Field)]
public sealed class AsGenericQueryAttribute(
    string? memberName = null, 
    bool useProperty = false,
    GeneratedGenericQueryAccessModifier modifier = GeneratedGenericQueryAccessModifier.Auto) : QueryAttribute
{
    public string? MemberName { get; } = memberName;
    public bool UseProperty { get; } = useProperty;
    public GeneratedGenericQueryAccessModifier Modifier { get; } = modifier;
}

public enum GeneratedGenericQueryAccessModifier
{
    Public,
    Private,
    Protected,
    Internal,
    ProtectedInternal,
    PrivateProtected,
    
    Auto
}