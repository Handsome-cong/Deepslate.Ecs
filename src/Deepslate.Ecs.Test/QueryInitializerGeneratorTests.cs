using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.SourceGenerator;
using Deepslate.Ecs.SourceGenerators;
using Deepslate.Ecs.Test.TestTickSystems;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit.Abstractions;

namespace Deepslate.Ecs.Test;

public sealed class QueryInitializerGeneratorTests(ITestOutputHelper outputHelper)
{
    private const string NameOfMultiQuerySystem = "MultiQuerySystem";

    private const string TextOfMultiQuerySystem = """
                                                  using Deepslate.Ecs.SourceGenerator;

                                                  namespace Deepslate.Ecs.Test.TestTickSystems;

                                                  public sealed partial class MultiQuerySystem : ITickSystemExecutor
                                                  {
                                                      [RequireWritable<Velocity>] [RequireReadOnly<Position>]
                                                      private Query _query1;
                                                  
                                                      [RequireInstantCommand]
                                                      [RequireWritable<Name>]
                                                      [AsGenericQuery(useProperty: true, memberName: "Query2", modifier: GeneratedGenericQueryAccessModifier.Public)]
                                                      private Query _query2;
                                                  
                                                      public MultiQuerySystem(TickSystemBuilder builder)
                                                      {
                                                          InitializeQuery(builder);
                                                      }
                                                  
                                                      public void Execute(TickSystemCommand command)
                                                      {
                                                          throw new NotImplementedException();
                                                      }
                                                  }
                                                  """;

    [Fact]
    public void MultiQuerySystemFromText()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(TextOfMultiQuerySystem);
        var compilation = CSharpCompilation.Create("Deepslate.Ecs.Test.TestTickSystems.MultiQuerySystem")
            .AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Query).Assembly.Location))
            .AddSyntaxTrees(syntaxTree);
        var driver = CSharpGeneratorDriver.Create(new QueryInitializerGenerator());
        var runResult = driver.RunGenerators(compilation).GetRunResult();

        var result = runResult.GeneratedTrees.Single(t => t.FilePath.EndsWith($"{NameOfMultiQuerySystem}.g.cs"));
        var generatedText = result.GetText().ToString();
        outputHelper.WriteLine(generatedText);
        var root = result.GetRoot() as CompilationUnitSyntax;
        var propertyDeclarationFound = root?.Members.Any(
            namespaceSyntax =>
            {
                if (namespaceSyntax is FileScopedNamespaceDeclarationSyntax fileScopedNamespaceSyntax)
                {
                    return fileScopedNamespaceSyntax.Members.Any(classSyntax =>
                    {
                        if (classSyntax is ClassDeclarationSyntax classDeclarationSyntax)
                        {
                            return classDeclarationSyntax.Members.Any(memberSyntax =>
                            {
                                if (memberSyntax is not PropertyDeclarationSyntax propertyDeclarationSyntax)
                                {
                                    return false;
                                }

                                Assert.Equal("Query2", propertyDeclarationSyntax.Identifier.Text);
                                Assert.Equal("public", propertyDeclarationSyntax.Modifiers.First().Text);
                                return true;
                            });
                        }

                        return false;
                    });
                }

                return false;
            }) ?? false;

        Assert.True(propertyDeclarationFound);
    }

    [Fact]
    public void GeneratedMultiQuerySystem()
    {
        var worldBuilder = new WorldBuilder()
            .WithArchetypeAndBuild<Velocity, Position, Name>();

        var stageBuilder = worldBuilder.AddStage();
        var tickSystemBuilder = stageBuilder.AddTickSystem();
        tickSystemBuilder.Build(new MultiQuerySystem(tickSystemBuilder), out var tickSystem);

        var queryFields = typeof(MultiQuerySystem)
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(fieldInfo => fieldInfo.FieldType == typeof(Query))
            .Where(fieldInfo => fieldInfo.GetCustomAttributes<QueryAttribute>().Any());

        var tickSystemBuilderByReflection = stageBuilder.AddTickSystem();
        foreach (var queryField in queryFields)
        {
            var queryBuilder = tickSystemBuilderByReflection.AddQuery();
            foreach (var attribute in queryField.GetCustomAttributes<QueryAttribute>())
            {
                if (attribute is RequireInstantCommandAttribute)
                {
                    queryBuilder.RequireInstantArchetypeCommand();
                    continue;
                }

                if (attribute is WithFilterAttribute withFilterAttribute)
                {
                    var candidateMethods = typeof(MultiQuerySystem).GetMethods(
                        BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    Predicate<Archetype>? predicate = null;
                    foreach (var candidateMethod in candidateMethods)
                    {
                        if (candidateMethod.Name != withFilterAttribute.Predicate)
                        {
                            continue;
                        }

                        predicate =
                            Delegate.CreateDelegate(typeof(Predicate<Archetype>), candidateMethod, false) as
                                Predicate<Archetype>;
                        if (predicate is not null)
                        {
                            break;
                        }
                    }

                    Assert.NotNull(predicate);
                    queryBuilder.WithFilter(predicate);
                    continue;
                }

                var type = attribute.GetType();
                if (!type.IsGenericType)
                {
                    return;
                }

                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(RequireReadOnlyAttribute<>))
                {
                    foreach (var componentType in type.GetGenericArguments())
                    {
                        queryBuilder.RequireReadOnly(componentType);
                    }
                }
                else if (genericTypeDefinition == typeof(RequireWritableAttribute<>))
                {
                    foreach (var componentType in type.GetGenericArguments())
                    {
                        queryBuilder.RequireWritable(componentType);
                    }
                }
                else if (genericTypeDefinition == typeof(WithAttribute<>))
                {
                    foreach (var componentType in type.GetGenericArguments())
                    {
                        queryBuilder.With(componentType);
                    }
                }
                else if (genericTypeDefinition == typeof(WithoutAttribute<>))
                {
                    foreach (var componentType in type.GetGenericArguments())
                    {
                        queryBuilder.Without(componentType);
                    }
                }
            }

            queryBuilder.Build(out _);
        }

        tickSystemBuilderByReflection.Build(new EmptySystem(), out var tickSystemByReflection);
        stageBuilder.Build();
        worldBuilder.Build();

        var span = MemoryMarshal.Cast<UsageCode, ulong>(tickSystem.UsageCodes);
        var spanByReflection = MemoryMarshal.Cast<UsageCode, ulong>(tickSystemByReflection.UsageCodes);
        var sb = new StringBuilder();
        foreach (var code in span)
        {
            sb.Append(code);
        }

        var codeString = sb.ToString();
        outputHelper.WriteLine(codeString);
        sb.Clear();
        foreach (var code in spanByReflection)
        {
            sb.Append(code);
        }

        var codeStringByReflection = sb.ToString();
        outputHelper.WriteLine(codeStringByReflection);
        Assert.Equal(codeStringByReflection, codeString);
    }
}