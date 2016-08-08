using UnityEngine;
using System.Collections;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int lodModifier)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshLodResoltuionModifier = (lodModifier == 0) ? 1 : 2 * lodModifier;
        int vertsPerLod = (width - 1) / meshLodResoltuionModifier + 1;

        MeshData meshData = new MeshData(vertsPerLod, vertsPerLod);
        int vertIndex = 0;

        for (int y = 0; y < height; y += meshLodResoltuionModifier)
        {
            for (int x = 0; x < width; x += meshLodResoltuionModifier)
            {
                var vertHeight = heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier;

                meshData.vertices[vertIndex] = new Vector3(topLeftX + x, vertHeight, topLeftZ - y);
                meshData.uvs[vertIndex] = new Vector2(x / (float)width, y / (float)height);

                if ((x < width - 1) && (y < height - 1))
                {
                    meshData.AddTriangle(vertIndex, vertIndex + vertsPerLod + 1, vertIndex + vertsPerLod);
                    meshData.AddTriangle(vertIndex + vertsPerLod + 1, vertIndex, vertIndex + 1);
                }

                vertIndex++;
            }
        }

        return meshData;
    }
}


public class MeshData
{

    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    public int vertIndex = 0;

    public MeshData(int meshWidth, int meshHeight)
    {

        vertices = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        uvs = new Vector2[meshWidth * meshHeight];
    }

    public void AddTriangle(int a, int b, int c)
    {

        triangles[vertIndex] = a;
        triangles[vertIndex + 1] = b;
        triangles[vertIndex + 2] = c;
        vertIndex += 3;
    }

    public Mesh CreateMesh()
    {

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }
}
