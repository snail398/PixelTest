using System;

[Serializable]
public class Vector 
{
    public float X;
    public float Y;

    public Vector(float X, float Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public float Length()
    {
        return (float)Math.Sqrt(X * X + Y * Y);
    }

    public static Vector operator *(Vector a, float v)
    {
        return new Vector(a.X * v, a.Y * v);
    }

    public static Vector operator +(Vector a, Vector b)
    {
        return new Vector(a.X + b.X, a.Y + b.Y);
    }

    public static Vector operator -(Vector a, Vector b)
    {
        return new Vector(a.X - b.X, a.Y - b.Y);
    }

    public static float Distance(Vector a, Vector b)
    {
        return (a - b).Length();
    }
}
