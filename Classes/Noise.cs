using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] GenerateNoise(int mapWidth, int mapHeight, float scale)
    {
        var noiseMap = new float[mapWidth,mapHeight];

        if (scale <= 0)
            scale = 0.0003f;

        var inverseScale = 1 / scale;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float perlinX = x * inverseScale;
                float perlinY = y * inverseScale;

                float perlinValue = Mathf.PerlinNoise(perlinX, perlinY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
