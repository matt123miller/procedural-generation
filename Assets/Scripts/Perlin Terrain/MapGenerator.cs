using UnityEngine;
using System.Collections;

public class MapGenerator : ProceduralGenerator
{

    public enum DrawMode { NoiseMap, ColourMap, MeshMap };
    public DrawMode drawMode;

    public const int mapChunkSize = 241;

    public float mapHeightMultiplier;

    public float noiseScale;
    [Range(0, 6)]
    public int meshLoD;

    [Range(1, 5)]
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public Vector2 offset;
    public AnimationCurve heightCurve;
    

    public TerrainType[] regions;

    public void Awake()
    {
        AsyncSceneTransition.ScreenFade.fadeOutFinished += RandomiseSeed;
        AsyncSceneTransition.ScreenFade.fadeOutFinished += Generate;
    }

    public override void Generate()
    {

        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        switch (drawMode)
        {

            case DrawMode.ColourMap:

                display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));

                break;

            case DrawMode.NoiseMap:

                display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));

                break;

            case DrawMode.MeshMap:

                var mesh = MeshGenerator.GenerateTerrainMesh(noiseMap, mapHeightMultiplier, heightCurve, meshLoD);
                var texture = TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize);

                display.DrawMesh(mesh, texture);

                break;
        }
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}