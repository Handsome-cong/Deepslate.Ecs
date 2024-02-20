namespace Deepslate.Ecs.SourceGenerators;

internal static class StringHelper
{
    public static string RemoveAttribute(string input)
    {
        const string attribute = "Attribute";
        return input.EndsWith(attribute) ? input.Substring(0, input.Length - attribute.Length) : input;
    }
}