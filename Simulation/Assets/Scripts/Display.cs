using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class Display : MonoBehaviour
{
    public Renderer textureRenderer;


    public Texture2D DrawPerlinNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0); int height = noiseMap.GetLength(1);
        meshFilter.sharedMesh.subMeshCount = GetComponent<MapGenerator>().biomesAmount;
        meshFilter.sharedMesh.SetTriangles(meshFilter.sharedMesh.triangles, 0);
        meshFilter.sharedMesh.RecalculateBounds();
        meshFilter.sharedMesh.RecalculateNormals();
        Texture2D texture = new Texture2D(width, height);
        Color[] colorMap = new Color[width * height];
        
        
        for (int x = 0; x < width; x++) 
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);


            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        // temporery
        textureRenderer.sharedMaterial = GetComponent<MapGenerator>().perlinMaterial;
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

        return texture;
    }

    public Texture2D DrawVoronoiMap(Color[,] colorMap)
    {
        textureRenderer.sharedMaterials = new Material[1];
        int width = colorMap.GetLength(0); int height = colorMap.GetLength(1);
        Texture2D texture = new Texture2D(width, height);
        meshFilter.sharedMesh.subMeshCount = GetComponent<MapGenerator>().biomesAmount;
        meshFilter.sharedMesh.SetTriangles(meshFilter.sharedMesh.triangles, 0);
        meshFilter.sharedMesh.RecalculateBounds();
        meshFilter.sharedMesh.RecalculateNormals();
        Color[] colors = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colors[x * width + y] = colorMap[x, y];
            }
        }

        texture.SetPixels(colors); texture.Apply();

        // temporery
        textureRenderer.sharedMaterial = GetComponent<MapGenerator>().vornoiMaterial;
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

        return texture;
    }

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawMesh(MeshData meshData, Color[,] colorMap)
    {
        DestroyImmediate(meshFilter.sharedMesh);
        meshFilter.sharedMesh = meshData.GenerateMesh();
        meshFilter.sharedMesh.subMeshCount = GetComponent<MapGenerator>().biomesAmount;
        List<Color> regions = CreateCollorTextureArray(colorMap);

        
        List<List<int>> subMeshList = new List<List<int>>();
        for (int i = 0; i < regions.Count; i++) 
        {
            subMeshList.Add(new List<int>());
        }

        for (int i = 0; i < meshFilter.sharedMesh.triangles.Length; i = i + 6)
        {
            Vector3 meanPlace = new Vector3();
            for (int j = 0; j < 6; j++)
            {
                meanPlace = meanPlace + meshFilter.sharedMesh.vertices[meshFilter.sharedMesh.triangles[i + j]];
            }
            
            meanPlace = meanPlace / 6 - new Vector3(0.5f, 0 , 0.5f);
            Color colour = colorMap[(int)meanPlace.x, (int)meanPlace.z];
            AssignTriangleToSubMesh(meshFilter.sharedMesh.triangles[i], meshFilter.sharedMesh.triangles[i + 1], meshFilter.sharedMesh.triangles[i + 2], regions.IndexOf(colour), subMeshList);
            AssignTriangleToSubMesh(meshFilter.sharedMesh.triangles[i + 3], meshFilter.sharedMesh.triangles[i + 4], meshFilter.sharedMesh.triangles[i + 5], regions.IndexOf(colour), subMeshList);
        }
        int materialindex = 0;
        foreach (List<int>subMesh in subMeshList)
        {
            meshFilter.sharedMesh.SetTriangles(subMesh.ToArray(), materialindex);
            materialindex++;
        }
        textureRenderer.sharedMaterials = GetComponent<MapGenerator>().biomeTextures;
    }
    
    public void AssignTriangleToSubMesh(int point1, int point2, int point3,int region, List<List<int>> subMeshList)
    {
        subMeshList[region].Add(point1);
        subMeshList[region].Add(point2);
        subMeshList[region].Add(point3);
    }
    public List<Color> CreateCollorTextureArray(Color[,] colorMap)
    {
        List<Color> colors = new List<Color>();
        for (int i = 0;i < colorMap.GetLength(0); i++)
        {
            for (int j = 0;j < colorMap.GetLength(1); j++)
            {
                if (!colors.Contains(colorMap[i, j]))
                {
                    colors.Add(colorMap[i, j]);
                }
            }
        }
        Debug.Log(colors.Count);
        return colors;
    }
}
