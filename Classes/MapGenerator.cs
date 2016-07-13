using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public float lacunarity;
    [Range(0,1)]
    public float persistance;
    public int octaves;
    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoise(mapWidth, mapHeight, noiseScale, seed, lacunarity, persistance, octaves, offset);

        // process noise in the middle to map a 3d terrain
        


        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    // Called automatically when any value is changed in the classes inspector
    // Simpler than handling it in MapGenEditor
    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 1)
        {
            octaves = 1;
        }
    }
}
