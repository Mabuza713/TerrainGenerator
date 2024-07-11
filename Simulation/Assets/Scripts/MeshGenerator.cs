using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float meshHeightMultiplier)
    {

        int width = heightMap.GetLength(1); int height = heightMap.GetLength(0);
        int vertIndex = 0;
        // instatiating mesh
        MeshData meshData = new MeshData(width, height);
    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                meshData.vertecies[vertIndex] = new Vector3(x, heightMap[x,y] * meshHeightMultiplier, y);
                meshData.uvs[vertIndex] = new Vector2(x / (float)width, y / (float)height); // we are casting width and height into floats so product of devision will be a float value
                if (x < width - 1 && y < height - 1) 
                {
                    // full squere
                    meshData.AddTraingle(vertIndex, vertIndex + width + 1, vertIndex + width);
                    meshData.AddTraingle(vertIndex + width + 1, vertIndex, vertIndex + 1);
                }
                vertIndex += 1;
            }
        }
        return meshData;
    }

}

public class MeshData
{
    public int[] triangles;
    public Vector3[] vertecies;
    public Vector2[] uvs;
    

    public MeshData(int width, int height)
    {
        vertecies = new Vector3[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
        uvs = new Vector2[width * height];
    }

    // there have to be an index of currently added vert and triangle
    int triaIndex = 0;


    public void AddTraingle(int point1, int point2, int point3)
    {
        triangles[triaIndex] = point1;
        triangles[triaIndex + 1] = point2;
        triangles[triaIndex + 2] = point3;

        triaIndex = triaIndex + 3;
    }

    public Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertecies;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }
}