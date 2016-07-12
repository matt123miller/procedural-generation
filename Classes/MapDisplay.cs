using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{

    public MeshRenderer texRender;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);


        var texture = new Texture2D(width, height);

        Color[] colourMap = new Color[width * height];

        // y loop first so that it completes each row in sequence instead of doing complete columns
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        texture.SetPixels(colourMap);
        texture.Apply();

        //texRender = GetComponent<Renderer>();
        texRender.sharedMaterial.mainTexture = texture;
        texRender.transform.localScale = new Vector3(width, 1, height);
    }
}
