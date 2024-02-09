namespace Deepslate.Ecs.Test;

public struct Position : IComponentData
{
    public float X;
    public float Y;
    public float Z;
}

public struct Velocity : IComponentData
{
    public float X;
    public float Y;
    public float Z;
}

public struct Name : IComponentData
{
    public string Value;
}

public struct Nested
{
    public Position Position;
    public Velocity Velocity;
    public Name Name;
}