using System;
using System.Linq;

using UnityEngine;
using System.Collections;

public static class FloatExtensions
{

    public static float RotationNormalizedDeg(this float rotation)
    {
        rotation = rotation % 360f;
        if (rotation < 0)
            rotation += 360f;
        return rotation;
    }

    public static float RotationNormalizedRad(this float rotation)
    {
        rotation = rotation % Mathf.PI;
        if (rotation < 0)
            rotation += Mathf.PI;
        return rotation;
    }

    public static float VolumeToDB(float volume)
    {
        if (volume > 0)
            return 20f * Mathf.Log10(volume);
        return -80f;
    }

    public static float DBToVolume(float db)
    {
        return Mathf.Pow(10f, db / 20f);
    }

    public static float AsRadian(this float angle)
    {
        return angle * Mathf.Deg2Rad;
    }

    public static float AsAngle(this float radian)
    {
        return radian * Mathf.Rad2Deg;
    }
}
