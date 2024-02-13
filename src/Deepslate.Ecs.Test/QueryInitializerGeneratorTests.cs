using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Deepslate.Ecs.Extensions;
using Deepslate.Ecs.SourceGenerator;
using Deepslate.Ecs.SourceGenerators;
using Deepslate.Ecs.Test.TestTickSystems;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit.Abstractions;

namespace Deepslate.Ecs.Test;

public sealed class QueryInitializerGeneratorTests(ITestOutputHelper outputHelper)
{
    private const string NameOfSystemWithSingleQueryProperty = "SystemWithSingleQueryProperty";

    private const string TextOfSystemWithSingleQueryProperty = """
                                                               using System.Diagnostics.CodeAnalysis;
                                                               using Deepslate.Ecs.SourceGenerators;

                                                               namespace Deepslate.Ecs.Test.TestTickSystems;

                                                               public sealed partial class SystemWithSingleQueryProperty : ITickSystemExecutor
                                                               {
                                                                   [field: RequireReadOnly<Position>]
                                                                   [field: RequireReadOnly<Position>]
                                                                   [field: WithFilter(nameof(Predicate))]
                                                                   public Query Query { get; private set; }
                                                               
                                                                   public SystemWithSingleQueryProperty(TickSystemBuilder builder)
                                                                   {
                                                                       InitializeQuery(builder);
                                                                   
                                                                   }
                                                               
                                                                   public void Execute()
                                                                   {
                                                                       throw new NotImplementedException();
                                                                   }
                                                               
                                                                   static bool Predicate(Archetype archetype) => false;

                                                               }
                                                               """;

    private const string ResultOfSystemWithSingleQueryProperty = """
                                                                 using System.Diagnostics.CodeAnalysis;

                                                                 namespace Deepslate.Ecs.Test.TestTickSystems;
                                                                 partial class SystemWithSingleQueryProperty
                                                                 {
                                                                     [MemberNotNull(nameof(Query))]
                                                                     private void InitializeQuery(TickSystemBuilder builder)
                                                                     {
                                                                        Query configuredQuery;
                                                                         builder.AddQuery()
                                                                             .RequireReadOnly<Position>()
                                                                             .WithFilter(Predicate)
                                                                             .Build(out configuredQuery);
                                                                         Query = configuredQuery;
                                                                     }
                                                                 }
                                                                 """;

    [Fact]
    public void SingleQueryProperty()
    {
        var generator = new QueryInitializerGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        var compilation = CSharpCompilation.Create(
            nameof(QueryInitializerGenerator),
            new[] { CSharpSyntaxTree.ParseText(TextOfSystemWithSingleQueryProperty) },
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Entity).Assembly.Location)
            });

        var runResult = driver.RunGenerators(compilation).GetRunResult();

        var generatedFileSyntax =
            runResult.GeneratedTrees.Single(tree =>
                tree.FilePath.EndsWith($"{NameOfSystemWithSingleQueryProperty}.g.cs"));

        var result = UnifyString(generatedFileSyntax.GetText().ToString());
        outputHelper.WriteLine(result);
        Assert.Equal(UnifyString(ResultOfSystemWithSingleQueryProperty), result);
    }


    private const string NameOfSystemWithDualQueryField = "SystemWithDualQueryField";

    private const string TextOfSystemWithDualQueryField = """
                                                          using System.Diagnostics.CodeAnalysis;
                                                          using Deepslate.Ecs.SourceGenerators;

                                                          namespace Deepslate.Ecs.Test.TestTickSystems;

                                                          public sealed partial class SystemWithDualQueryField : ITickSystemExecutor
                                                          {
                                                              [RequireReadOnly<Position>]
                                                              [RequireWritable<Position>]
                                                              [WithFilter(nameof(Predicate))]
                                                              private Query _query;
                                                              
                                                              [RequireReadOnly<Velocity>]
                                                              [RequireWritable<Position>]
                                                              private Query _anotherQuery;
                                                          
                                                              public SystemWithDualQueryField(TickSystemBuilder builder)
                                                              {
                                                                  InitializeQuery(builder);
                                                              
                                                              }
                                                          
                                                              public void Execute()
                                                              {
                                                                  throw new NotImplementedException();
                                                              }
                                                          
                                                              static bool Predicate(Archetype archetype) => false;

                                                          }
                                                          """;

    private const string ResultOfSystemWithDualQueryField = """
                                                            using System.Diagnostics.CodeAnalysis;

                                                            namespace Deepslate.Ecs.Test.TestTickSystems;
                                                            partial class SystemWithDualQueryField
                                                            {
                                                                [MemberNotNull(nameof(_query))]
                                                                [MemberNotNull(nameof(_anotherQuery))]
                                                                private void InitializeQuery(TickSystemBuilder builder)
                                                                {
                                                                   Query configuredQuery;
                                                                    builder.AddQuery()
                                                                        .RequireWritable<Position>()
                                                                        .WithFilter(Predicate)
                                                                        .Build(out configuredQuery);
                                                                    _query = configuredQuery;
                                                                    builder.AddQuery()
                                                                        .RequireWritable<Position>()
                                                                        .RequireReadOnly<Velocity>()
                                                                        .Build(out configuredQuery);
                                                                    _anotherQuery = configuredQuery;
                                                                }
                                                            }
                                                            """;

    [Fact]
    public void DualQueryField()
    {
        var generator = new QueryInitializerGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        var compilation = CSharpCompilation.Create(
            nameof(QueryInitializerGenerator),
            new[] { CSharpSyntaxTree.ParseText(TextOfSystemWithDualQueryField) },
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Entity).Assembly.Location)
            });

        var runResult = driver.RunGenerators(compilation).GetRunResult();

        var generatedFileSyntax =
            runResult.GeneratedTrees.Single(tree => tree.FilePath.EndsWith($"{NameOfSystemWithDualQueryField}.g.cs"));

        var result = UnifyString(generatedFileSyntax.GetText().ToString());
        outputHelper.WriteLine(result);
        Assert.Equal(UnifyString(ResultOfSystemWithDualQueryField), result);
    }

    private static string UnifyString(string text) =>
        text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

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