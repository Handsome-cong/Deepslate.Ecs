using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Deepslate.Ecs.SourceGenerators;

[Generator]
public sealed class QueryInitializerGenerator : IIncrementalGenerator
{
    private const string RequireWritableAttribute = "RequireWritable";
    private const string RequireReadOnlyAttribute = "RequireReadOnly";
    private const string WithAttribute = "With";
    private const string WithoutAttribute = "Without";
    private const string WithFilterAttribute = "WithFilter";
    private const string RequireInstantCommandAttribute = "RequireInstantCommand";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider
            .CreateSyntaxProvider(
                (s, _) => s is ClassDeclarationSyntax,
                (ctx, _) => GetTickSystemsForGeneration(ctx))
            .Where(tickSystem => tickSystem.QueryMembers.Count > 0);

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
                            result.QueryMembers.Add(queryMember);
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
                        result.QueryMembers.Add(queryMember);
                    }

                    break;
                }
            }
        }

        return result;
    }

    private static bool TryGetQueryAttributeInfo(
        SyntaxList<AttributeListSyntax> attributeLists,
        string memberName,
        GeneratorSyntaxContext context,
        out QueryMember queryMember)
    {
        queryMember = new QueryMember(memberName);
        foreach (var attributeListSyntax in attributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var attributeName = attributeSyntax.Name.ToString();
                if (attributeName.Contains(RequireReadOnlyAttribute))
                {
                    FillComponentTypes(queryMember.ReadOnly, attributeSyntax);
                }
                else if (attributeName.Contains(RequireWritableAttribute))
                {
                    FillComponentTypes(queryMember.Writable, attributeSyntax);
                }
                else if (attributeName.Contains(WithFilterAttribute))
                {
                    var predicateSyntax = attributeSyntax.ArgumentList?.Arguments[0].Expression;
                    if (predicateSyntax != null)
                    {
                        queryMember.Predicate =
                            context.SemanticModel.GetOperation(predicateSyntax)?.ConstantValue.Value as string ??
                            string.Empty;
                    }
                }
                else if (attributeName.Contains(WithAttribute))
                {
                    FillComponentTypes(queryMember.With, attributeSyntax);
                }
                else if (attributeName.Contains(WithoutAttribute))
                {
                    FillComponentTypes(queryMember.Without, attributeSyntax);
                }
                else if (attributeName.Contains(RequireInstantCommandAttribute))
                {
                    queryMember.InstantCommand = true;
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

            foreach (var queryMember in tickSystem.QueryMembers)
            {
                GenerateNotNullAttribute(attributeStringBuilder, queryMember);
                GenerateQueryBuilder(methodStringBuilder, queryMember);
            }

            var code = $$"""
                         using System.Diagnostics.CodeAnalysis;

                         namespace {{namespaceName}};
                         partial class {{className}}
                         {{{attributeStringBuilder}}
                             private void InitializeQuery(TickSystemBuilder builder)
                             {
                                Query configuredQuery;{{methodStringBuilder}}
                             }
                         }
                         """;

            context.AddSource($"{className}.g.cs", SourceText.From(code, Encoding.UTF8));
        }
    }

    private static void GenerateNotNullAttribute(StringBuilder currentStringBuilder, QueryMember queryMember)
    {
        currentStringBuilder.Append($"\n    [MemberNotNull(nameof({queryMember.Name}))]");
    }

    private static void GenerateQueryBuilder(
        StringBuilder currentStringBuilder,
        QueryMember queryMember)
    {
        // TODO: Add an analyzer to check if the attribute is used correctly
        currentStringBuilder.Append("\n        builder.AddQuery()\n");

        foreach (var writableComponent in queryMember.Writable)
        {
            currentStringBuilder.Append($"            .RequireWritable<{writableComponent}>()\n");
        }

        foreach (var readOnlyComponent in queryMember.ReadOnly.Where(readOnlyComponent =>
                     !queryMember.Writable.Contains(readOnlyComponent)))
        {
            currentStringBuilder.Append($"            .RequireReadOnly<{readOnlyComponent}>()\n");
        }

        foreach (var withComponent in queryMember.With.Where(withComponent =>
                     !queryMember.ReadOnly.Contains(withComponent) && !queryMember.Writable.Contains(withComponent)))
        {
            currentStringBuilder.Append($"            .With<{withComponent}>()\n");
        }

        foreach (var withoutComponent in queryMember.Without)
        {
            currentStringBuilder.Append($"            .Without<{withoutComponent}>()\n");
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
    }

    private sealed class QueryMember(string name)
    {
        public readonly HashSet<string> ReadOnly = [];
        public readonly HashSet<string> Writable = [];
        public readonly HashSet<string> With = [];
        public readonly HashSet<string> Without = [];
        public string Predicate = string.Empty;
        public bool InstantCommand;
        public readonly string Name = name;

        public bool HasAttribute()
        {
            var result = false;
            result |= ReadOnly.Count > 0;
            result |= Writable.Count > 0;
            result |= With.Count > 0;
            result |= Without.Count > 0;
            result |= !string.IsNullOrWhiteSpace(Predicate);
            result |= InstantCommand;
            return result;
        }
    }

    private sealed class TickSystemExecutorClass(ClassDeclarationSyntax classDeclarationSyntax)
    {
        public readonly List<QueryMember> QueryMembers = [];
        public readonly ClassDeclarationSyntax ClassDeclarationSyntax = classDeclarationSyntax;
    }
}