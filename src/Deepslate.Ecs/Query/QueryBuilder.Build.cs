using System.Diagnostics.Contracts;

namespace Deepslate.Ecs;

public partial class QueryBuilder
{
    /// <summary>
    /// Call this to complete the query configuration and register it to the system.
    /// As long as this method is called, the operations on this builder will not affect the query anymore.
    /// </summary>
    /// <param name="configuredQuery">
    /// The query that has been configured and registered.
    /// </param>
    /// <seealso cref="Result"/>
    public TickSystemBuilder Build(out Query configuredQuery)
    {
        if (Result is not null)
        {
            configuredQuery = Result;
        }

        var query = new Query(
            GetMatchedArchetypes(),
            RequiredWritableComponentTypes,
            RequiredReadOnlyComponentTypes,
            _requireInstantArchetypeCommand);

        Result = query;
        configuredQuery = query;
        TickSystemBuilder.RegisterQuery(query);
        return TickSystemBuilder;
    }

    private Archetype[] GetMatchedArchetypes()
    {
        var worldBuilder = TickSystemBuilder.StageBuilder.WorldBuilder;
        var allArchetypes = worldBuilder.Archetypes;
        var componentTypeToArchetypeIndices = worldBuilder.ComponentTypeToArchetypeIds;
        var counters = new int[allArchetypes.Count];
        foreach (var componentType in RequiredWritableComponentTypes)
        {
            var indices = componentTypeToArchetypeIndices[componentType];
            foreach (var index in indices)
            {
                counters[index]++;
            }
        }

        foreach (var componentType in RequiredReadOnlyComponentTypes)
        {
            var indices = componentTypeToArchetypeIndices[componentType];
            foreach (var index in indices)
            {
                counters[index]++;
            }
        }

        foreach (var componentType in IncludedComponentTypes)
        {
            var indices = componentTypeToArchetypeIndices[componentType];
            foreach (var index in indices)
            {
                counters[index]++;
            }
        }

        foreach (var componentType in ExcludedComponentTypes)
        {
            var indices = componentTypeToArchetypeIndices[componentType];
            foreach (var index in indices)
            {
                counters[index]--;
            }
        }

        var requiredComponentTypeCount =
            RequiredWritableComponentTypes.Count + RequiredReadOnlyComponentTypes.Count + IncludedComponentTypes.Count;
        List<int> matchedIndices = [];
        for (var index = 0; index < counters.Length; index++)
        {
            if (counters[index] == requiredComponentTypeCount && (_filter?.Invoke(allArchetypes[index]) ?? true))
            {
                matchedIndices.Add(index);
            }
        }

        var result = new Archetype[matchedIndices.Count];
        for (var index = 0; index < matchedIndices.Count; index++)
        {
            result[index] = allArchetypes[matchedIndices[index]];
        }

        return result;
    }
}