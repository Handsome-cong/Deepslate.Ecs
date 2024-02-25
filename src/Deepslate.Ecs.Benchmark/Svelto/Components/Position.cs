using Svelto.ECS;

namespace Deepslate.Ecs.Benchmark.Svelto.Components;

public struct Position : IEntityComponent
{
    public int X, Y, Z;
}


public sealed class PositionDescriptor : GenericEntityDescriptor<Position>;