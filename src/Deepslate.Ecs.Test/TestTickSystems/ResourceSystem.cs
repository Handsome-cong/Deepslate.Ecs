namespace Deepslate.Ecs.Test.TestTickSystems;

public class ResourceSystem<TResource> : ITickSystemExecutor
    where TResource : IResource
{
    public ResourceSystem(TickSystemBuilder builder)
    {
        builder.WithResource<TResource>();
    }

    public void Execute(Command command)
    {
        command.GetResource<TResource>();
    }
}


public class ResourceSystemRetrieveOnly<TResource> : ITickSystemExecutor
    where TResource : IResource
{
    public ResourceSystemRetrieveOnly(TickSystemBuilder _)
    {
    }

    public void Execute(Command command)
    {
        command.GetResource<TResource>();
    }
}