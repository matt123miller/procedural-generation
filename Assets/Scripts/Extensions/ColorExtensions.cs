using UnityEngine;
using System.Collections;

public static class ColorExtensions 
{
    public static Color WithAlpha(this Color color, float alpha) {
        color.a = alpha;
        return color;
    }
}
