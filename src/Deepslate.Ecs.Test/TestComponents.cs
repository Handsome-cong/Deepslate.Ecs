namespace Deepslate.Ecs.Test;

public struct Position : IComponent
{
    public float X;
    public float Y;
    public float Z;

    public static readonly Position One = new() { X = 1, Y = 1, Z = 1 };
}

public struct Velocity : IComponent
{
    public float X;
    public float Y;
    public float Z;
}

public struct Name : IComponent
{
    public string Value;
}

public struct Nested
{
    public Position Position;
    public Velocity Velocity;
    public Name Name;
}