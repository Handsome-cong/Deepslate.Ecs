namespace Deepslate.Ecs.Test.TestResources;

public sealed record CounterResource(int Value) : IResource;

public sealed class CounterResourceFactory : IResourceFactory<CounterResource>
{
    private int _count;
    public CounterResource Create() => new(_count++);
}