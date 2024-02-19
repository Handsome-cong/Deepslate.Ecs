using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Deepslate.Ecs.SourceGenerators;

[Generator]
public sealed class QueryInitializerGenerator : IIncrementalGenerator
{
    private const string WithWritableAttribute = "WithWritable";
    private const string WithReadOnlyAttribute = "WithReadOnly";
    private const string WithAttribute = "WithIncluded";
    private const string WithExcludedAttribute = "WithExcluded";
    private const string WithFilterAttribute = "WithFilter";
    private const string RequireInstantCommandAttribute = "RequireInstantCommand";
    private const string AsGenericQueryAttribute = "AsGenericQuery";

    private static readonly Regex GenericQueryQualifier = new(@"Writable(?:<.+>)?\.ReadOnly(?:<.+>)?\.Query");

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider
            .CreateSyntaxProvider(
                (s, _) => s is ClassDeclarationSyntax,
                (ctx, _) => GetTickSystemsForGeneration(ctx))
            .Where(tickSystem => tickSystem.NeedsGeneration);

        context.RegisterSourceOutput(
            context.CompilationProvider.Combine(provider.Collect()),
            (ctx, tuple) => GenerateCode(ctx, tuple.Left, tuple.Right));
    }

    private static TickSystemExecutorClass GetTickSystemsForGeneration(
        GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        var isTickSystem = classDeclarationSyntax.BaseList?.Types.Any(syntax =>
        {
            if (syntax.Type is not IdentifierNameSyntax identifierNameSyntax)
            {
                return false;
            }

            return identifierNameSyntax.Identifier.Text == "ITickSystemExecutor";
        }) ?? false;

        var result = new TickSystemExecutorClass(classDeclarationSyntax);
        if (!isTickSystem)
        {
            return result;
        }

        foreach (var member in classDeclarationSyntax.Members)
        {
            switch (member)
            {
                case FieldDeclarationSyntax fieldDeclarationSyntax:
                {
                    foreach (var variableName in fieldDeclarationSyntax.Declaration.Variables.Select(variable =>
                                 variable.Identifier.Text))
                    {
                        if (TryGetQueryAttributeInfo(
                                fieldDeclarationSyntax.AttributeLists,
                                variableName,
                                context,
                                out var queryMember))
                        {
                            if (string.IsNullOrWhiteSpace(queryMember.AccessModifier))
                            {
                                queryMember.AccessModifier = GetAccessModifier(fieldDeclarationSyntax.Modifiers);
                            }

                            result.QueryMembers.Add(queryMember);
                        }
                        else if (GenericQueryQualifier.IsMatch(fieldDeclarationSyntax.Declaration.Type.ToString()))
                        {
                            result.GenericQueryMembers.Add(new GenericQueryMember(variableName));
                        }
                    }

                    break;
                }
                case PropertyDeclarationSyntax propertyDeclarationSyntax:
                {
                    if (TryGetQueryAttributeInfo(propertyDeclarationSyntax.AttributeLists,
                            propertyDeclarationSyntax.Identifier.Text,
                            context,
                            out var queryMember))
                    {
                        if (string.IsNullOrWhiteSpace(queryMember.AccessModifier))
                        {
                            queryMember.AccessModifier = GetAccessModifier(propertyDeclarationSyntax.Modifiers);
                        }
                        else if (GenericQueryQualifier.IsMatch(propertyDeclarationSyntax.Type.ToString()))
                        {
                            result.GenericQueryMembers.Add(
                                new GenericQueryMember(propertyDeclarationSyntax.Identifier.Text));
                        }

                        result.QueryMembers.Add(queryMember);
                    }

                    break;
                }
            }
        }

        return result;
    }

    private static string GetAccessModifier(SyntaxTokenList modifiers)
    {
        var sb = new StringBuilder();
        if (modifiers.Any(syntax => syntax.Text == "public"))
        {
            sb.Append("public");
        }

        if (modifiers.Any(syntax => syntax.Text == "private"))
        {
            sb.Append("private ");
        }

        if (modifiers.Any(syntax => syntax.Text == "protected"))
        {
            sb.Append("protected ");
        }

        if (modifiers.Any(syntax => syntax.Text == "internal"))
        {
            sb.Append("internal");
        }

        return sb.ToString().Trim();
    }

    private static bool TryGetQueryAttributeInfo(
        SyntaxList<AttributeListSyntax> attributeLists,
        string memberName,
        GeneratorSyntaxContext context,
        out QueryMember queryMember)
    {
        queryMember = new QueryMember(memberName);
        var semanticModel = context.SemanticModel;
        foreach (var attributeListSyntax in attributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var attributeName = attributeSyntax.Name.ToString();
                if (attributeName.Contains(WithReadOnlyAttribute))
                {
                    FillComponentTypes(queryMember.ReadOnly, attributeSyntax);
                }
                else if (attributeName.Contains(WithWritableAttribute))
                {
                    FillComponentTypes(queryMember.Writable, attributeSyntax);
                }
                else if (attributeName.Contains(WithFilterAttribute))
                {
                    var predicateSyntax = attributeSyntax.ArgumentList?.Arguments[0].Expression;
                    if (predicateSyntax != null)
                    {
                        queryMember.Predicate =
                            semanticModel.GetOperation(predicateSyntax)?.ConstantValue.Value as string ?? string.Empty;
                    }
                }
                else if (attributeName.Contains(WithAttribute))
                {
                    FillComponentTypes(queryMember.WithIncluded, attributeSyntax);
                }
                else if (attributeName.Contains(WithExcludedAttribute))
                {
                    FillComponentTypes(queryMember.WithExcluded, attributeSyntax);
                }
                else if (attributeName.Contains(RequireInstantCommandAttribute))
                {
                    queryMember.InstantCommand = true;
                }
                else if (attributeName.Contains(AsGenericQueryAttribute))
                {
                    queryMember.AsGenericQuery = true;
                    var argumentList = attributeSyntax.ArgumentList;
                    if (argumentList is null)
                    {
                        continue;
                    }

                    for (var i = 0; i < argumentList.Arguments.Count; i++)
                    {
                        var argument = argumentList.Arguments[i];
                        var nameColon = argument.NameColon?.Name.ToString();
                        var expression = argument.Expression;
                        switch (nameColon, i)
                        {
                            case ("memberName", _) or (null, 0):
                            {
                                if (semanticModel.GetOperation(expression)?.ConstantValue.Value is string newMemberName)
                                {
                                    queryMember.GenericName = newMemberName;
                                }

                                break;
                            }
                            case ("useProperty", _) or (null, 1):
                            {
                                if (semanticModel.GetOperation(expression)?.ConstantValue.Value is bool useProperty)
                                {
                                    queryMember.UseProperty = useProperty;
                                }

                                break;
                            }
                            case ("modifier", _) or (null, 2):
                            {
                                if (semanticModel.GetOperation(expression)?.ConstantValue.Value is int modifier)
                                {
                                    var modifierStr = modifier switch
                                    {
                                        0 => "public",
                                        1 => "private",
                                        2 => "protected",
                                        3 => "internal",
                                        4 => "protected internal",
                                        5 => "private protected",
                                        _ => null,
                                    };
                                    if (modifierStr is not null)
                                    {
                                        queryMember.AccessModifier = modifierStr;
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }

        return queryMember.HasAttribute();
    }

    private static void FillComponentTypes(HashSet<string> componentTypes, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax.Name is not GenericNameSyntax genericNameSyntax)
        {
            return;
        }

        foreach (var componentType in genericNameSyntax.TypeArgumentList.Arguments.Select(componentType =>
                     componentType.ToString()))
        {
            componentTypes.Add(componentType);
        }
    }

    private static void GenerateCode(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<TickSystemExecutorClass> tickSystems)
    {
        foreach (var tickSystem in tickSystems)
        {
            var classDeclarationSyntax = tickSystem.ClassDeclarationSyntax;
            var semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
            {
                continue;
            }

            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            var className = classDeclarationSyntax.Identifier.Text;

            var attributeStringBuilder = new StringBuilder();
            var methodStringBuilder = new StringBuilder();
            var genericQueryFields = new StringBuilder();
            var genericQueryInitialize = new StringBuilder();

            foreach (var queryMember in tickSystem.QueryMembers)
            {
                GenerateGenericQueryField(genericQueryFields, queryMember);
                GenerateNotNullAttribute(attributeStringBuilder, queryMember);
                GenerateQueryBuilder(methodStringBuilder, queryMember);
                if (queryMember.AsGenericQuery)
                {
                    GenerateGenericQueryInitialize(genericQueryInitialize, queryMember.GenericName);
                }
            }

            foreach (var genericQueryMember in tickSystem.GenericQueryMembers)
            {
                GenerateGenericQueryInitialize(genericQueryInitialize, genericQueryMember.Name);
            }

            var genericNameSpace =
                genericQueryFields.Length > 0 ? "using Deepslate.Ecs.GenericWrapper;\n" : string.Empty;

            var initializeQuery = methodStringBuilder.Length == 0
                ? string.Empty
                : $$"""
                    {{genericQueryFields}}
                    {{attributeStringBuilder}}
                        private void InitializeQuery(TickSystemBuilder builder)
                        {
                            Query configuredQuery;{{methodStringBuilder}}
                        }
                    """;

            var postInitialize = genericQueryInitialize.Length == 0
                ? string.Empty
                : $$"""
                        void ITickSystemExecutor.PostInitialize(World world)
                        {{{genericQueryInitialize}}
                        }
                    """;

            var code = $$"""
                         using System.Diagnostics.CodeAnalysis;
                         {{genericNameSpace}}
                         namespace {{namespaceName}};

                         partial class {{className}}
                         {{{initializeQuery}}

                         {{postInitialize}}
                         }
                         """;

            context.AddSource($"{className}.g.cs", SourceText.From(code, Encoding.UTF8));
        }
    }

    private static void GenerateGenericQueryField(StringBuilder currentStringBuilder, QueryMember queryMember)
    {
        if (!queryMember.AsGenericQuery)
        {
            return;
        }

        var readOnly = string.Join(", ", queryMember.ReadOnly);
        if (queryMember.ReadOnly.Count > 0)
        {
            readOnly = $"<{readOnly}>";
        }

        var writable = string.Join(", ", queryMember.Writable);
        if (queryMember.Writable.Count > 0)
        {
            writable = $"<{writable}>";
        }


        var setterModifier = queryMember.AccessModifier == "private" ? string.Empty : "private ";
        var endOfDeclaration = queryMember.UseProperty ? $$"""{ get; {{setterModifier}}set; }""" : ";";
        currentStringBuilder.Append(
            $"\n    {queryMember.AccessModifier} Writable{writable}.ReadOnly{readOnly}.Query {queryMember.GenericName} {endOfDeclaration}");
    }

    private static void GenerateNotNullAttribute(StringBuilder currentStringBuilder, QueryMember queryMember)
    {
        currentStringBuilder.Append($"\n    [MemberNotNull(nameof({queryMember.Name}))]");
        if (queryMember.AsGenericQuery)
        {
            currentStringBuilder.Append($"\n    [MemberNotNull(nameof({queryMember.GenericName}))]");
        }
    }

    private static void GenerateQueryBuilder(
        StringBuilder currentStringBuilder,
        QueryMember queryMember)
    {
        // Maybe we should add an analyzer to check if the attribute is used correctly
        // * WithWritable, WithReadOnly, WithIncluded, WithExcluded should not be used with the same component type
        // * WithFilter should be used with a valid predicate method name
        currentStringBuilder.Append("\n        builder.AddQuery()\n");

        foreach (var writableComponent in queryMember.Writable)
        {
            currentStringBuilder.Append($"            .WithWritable<{writableComponent}>()\n");
        }

        foreach (var readOnlyComponent in queryMember.ReadOnly.Where(readOnlyComponent =>
                     !queryMember.Writable.Contains(readOnlyComponent)))
        {
            currentStringBuilder.Append($"            .WithReadOnly<{readOnlyComponent}>()\n");
        }

        foreach (var withIncludedComponent in queryMember.WithIncluded.Where(withComponent =>
                     !queryMember.ReadOnly.Contains(withComponent) && !queryMember.Writable.Contains(withComponent)))
        {
            currentStringBuilder.Append($"            .WithIncluded<{withIncludedComponent}>()\n");
        }

        foreach (var withExcludedComponent in queryMember.WithExcluded)
        {
            currentStringBuilder.Append($"            .WithExcluded<{withExcludedComponent}>()\n");
        }

        if (!string.IsNullOrWhiteSpace(queryMember.Predicate))
        {
            currentStringBuilder.Append($"            .WithFilter({queryMember.Predicate})\n");
        }

        if (queryMember.InstantCommand)
        {
            currentStringBuilder.Append("            .RequireInstantArchetypeCommand()\n");
        }

        currentStringBuilder.Append("            .Build(out configuredQuery);\n");

        currentStringBuilder.Append($"        {queryMember.Name} = configuredQuery;");
        if (queryMember.AsGenericQuery)
        {
            currentStringBuilder.Append($"        {queryMember.GenericName} = new({queryMember.Name});\n");
        }
    }

    private static void GenerateGenericQueryInitialize(
        StringBuilder currentStringBuilder,
        string memberNames)
    {
        currentStringBuilder.Append($"\n        {memberNames}.PostInitialize();");
    }

    private sealed class QueryMember(string name)
    {
        public readonly HashSet<string> ReadOnly = [];
        public readonly HashSet<string> Writable = [];
        public readonly HashSet<string> WithIncluded = [];
        public readonly HashSet<string> WithExcluded = [];
        public string Predicate = string.Empty;
        public bool InstantCommand;

        public bool AsGenericQuery;

        public readonly string Name = name;

        public string GenericName = $"{name}Generic";
        public string AccessModifier = string.Empty;
        public bool UseProperty;

        public bool HasAttribute()
        {
            var result = false;
            result |= ReadOnly.Count > 0;
            result |= Writable.Count > 0;
            result |= WithIncluded.Count > 0;
            result |= WithExcluded.Count > 0;
            result |= !string.IsNullOrWhiteSpace(Predicate);
            result |= InstantCommand;
            return result;
        }
    }

    private sealed class GenericQueryMember(string name)
    {
        public readonly string Name = name;
    }

    private sealed class TickSystemExecutorClass(ClassDeclarationSyntax classDeclarationSyntax)
    {
        public readonly List<QueryMember> QueryMembers = [];
        public readonly List<GenericQueryMember> GenericQueryMembers = [];
        public readonly ClassDeclarationSyntax ClassDeclarationSyntax = classDeclarationSyntax;

        public bool NeedsGeneration => QueryMembers.Count > 0 || GenericQueryMembers.Count > 0;
    }
}