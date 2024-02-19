using Deepslate.Ecs.Util;

namespace Deepslate.Ecs.Test;

public sealed class UtilTests
{
    [DisabledFact]
    public void IsUnmanaged()
    {
        Assert.True(UnmanagedHelper.IsUnmanaged(typeof(Position)));
        Assert.True(UnmanagedHelper.IsUnmanaged<Velocity>());
        Assert.False(UnmanagedHelper.IsUnmanaged(typeof(Name)));
        Assert.False(UnmanagedHelper.IsUnmanaged<Nested>());
    }
    
    [DisabledFact]
    public void GuardClauses()
    {
        Guard.IsComponent(typeof(Position));
        Guard.IsComponent(typeof(Velocity));
        Guard.IsComponent(typeof(Name));

        Assert.Throws<ArgumentException>(() => Guard.IsComponent(typeof(Nested)));
        
        Guard.IsUnmanaged(typeof(Position));
        Guard.IsUnmanaged(typeof(Velocity));
        
        Assert.Throws<ArgumentException>(() => Guard.IsUnmanaged(typeof(Name)));
        Assert.Throws<ArgumentException>(() => Guard.IsUnmanaged(typeof(Nested)));
    }
}