namespace Deepslate.Ecs;

public partial class QueryBuilder
{
    public Query Build()
    {
        var matchedArchetypes = GetMatchedArchetypes();

        var result = new Query(
            matchedArchetypes,
            RequiredWritableComponentTypes.ToArray());

        return result;
    }

    private Archetype[] GetMatchedArchetypes()
    {
        var allArchetypes = World.Archetypes;
        var componentTypeToArchetypeIndices = World.ComponentTypeToArchetypeIndices;
        Span<int> counters = new int[allArchetypes.Count];
        foreach (var componentType in RequiredWritableComponentTypes)
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

        var requiredComponentTypeCount = RequiredWritableComponentTypes.Count;
        List<int> matchedIndices = [];
        for (int index = 0; index < counters.Length; index++)
        {
            if (counters[index] == requiredComponentTypeCount && Filters.All(filter => filter(allArchetypes[index])))
            {
                matchedIndices.Add(index);
            }
        }

        var result = new Archetype[matchedIndices.Count];
        for (int index = 0; index < matchedIndices.Count; index++)
        {
            result[index] = allArchetypes[matchedIndices[index]];
        }

        return result;
    }
}