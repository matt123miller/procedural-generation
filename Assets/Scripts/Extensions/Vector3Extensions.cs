using UnityEngine;
using System.Collections;

public static class Vector3Extensions
{
    // None of these compass point things work by calling e.g. Vector3.West() because you can't extend
    // the Vector3 class with a static, standalone, function, only add new instance methods.
    public static Vector3 North()
    {
        return Vector3.forward;
    }

    public static Vector3 South()
    {
        return Vector3.back;
    }

    public static Vector3 East()
    {
        return Vector3.right;
    }

    public static Vector3 West()
    {
        return Vector3.left;
    }

    public static Vector3[] CardinalDirections(this Vector3 vector3)
    {
        return new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    }

    public static Vector2 To2(this Vector3 vector)
    {
        return vector;
    }

    public static Vector3 WithX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 WithY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 WithZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 PlusX(this Vector3 vector, float plusX)
    {
        return new Vector3(vector.x + plusX, vector.y, vector.z);
    }

    public static Vector3 PlusY(this Vector3 vector, float plusY)
    {
        return new Vector3(vector.x, vector.y + plusY, vector.z);
    }

    public static Vector3 PlusZ(this Vector3 vector, float plusZ)
    {
        return new Vector3(vector.x, vector.y, vector.z + plusZ);
    }

    public static Vector3 TimesX(this Vector3 vector, float timesX)
    {
        return new Vector3(vector.x * timesX, vector.y, vector.z);
    }

    public static Vector3 TimesY(this Vector3 vector, float timesY)
    {
        return new Vector3(vector.x, vector.y * timesY, vector.z);
    }

    public static Vector3 TimesZ(this Vector3 vector, float timesZ)
    {
        return new Vector3(vector.x, vector.y, vector.z * timesZ);
    }
}
