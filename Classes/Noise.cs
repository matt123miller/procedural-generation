using UnityEngine;
using System.Collections;


public static class Noise
{

    public static float[,] GenerateNoise(int mapWidth, int mapHeight, float scale, int seed, float lacunarity, float persistance, int octaves, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;


        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
        //        var noiseMap = new float[mapWidth, mapHeight];

        //        System.Random prng = new System.Random(seed);

        //        Vector2[] octaveOffsets = new Vector2[octaves];

        //        for (int i = 0; i < octaves; i++)
        //        {
        //            var offsetX = prng.Next(-1000, 1000) + offset.x;
        //            var offsetY = prng.Next(-1000, 1000) + offset.y;
        //            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        //        }

        //        if (scale <= 0)
        //            scale = 0.0003f;

        //        float inverseScale = 1 / scale;
        //        float halfWidth = mapWidth / 2;
        //        float halfHeight = mapHeight / 2;

        //        var minNoiseHeight = float.MaxValue;
        //        var maxNoiseHeight = float.MinValue;

        //        for (int y = 0; y < mapHeight; y++)
        //        {
        //            for (int x = 0; x < mapWidth; x++)
        //            {
        //                float amplitude = 1f;
        //                float frequency = 1f;
        //                float noiseHeight = 0f;

        //                for (int o = 0; 0 < octaves; o++)
        //                {
        //                    float perlinX = (x - halfWidth) * inverseScale * frequency + octaveOffsets[o].x;
        //                    float perlinY = (y - halfHeight) * inverseScale * frequency + octaveOffsets[o].y;

        //                    float perlinValue = Mathf.PerlinNoise(perlinX, perlinY) * 2 - 1;
        //                    noiseHeight += perlinValue * amplitude;

        //                    amplitude *= persistance;
        //                    frequency *= lacunarity;

        //                }

        //                if (noiseHeight > maxNoiseHeight)
        //                {
        //                    maxNoiseHeight = noiseHeight;
        //                }
        //                else if (noiseHeight < minNoiseHeight)
        //                {
        //                    minNoiseHeight = noiseHeight;
        //                }


        //                noiseMap[x, y] = noiseHeight;

        //            }
        //        }


        //        for (int y = 0; y < mapHeight; y++)
        //        {
        //            for (int x = 0; x < mapWidth; x++)
        //            {
        //                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
        //            }
        //        }


        //        return noiseMap;
    }
}
