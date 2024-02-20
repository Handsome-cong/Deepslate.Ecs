namespace Deepslate.Ecs.Test.TestResources;

public interface IResourceFactory<out TResource>
    where TResource : IResource
{
    TResource Create();
}