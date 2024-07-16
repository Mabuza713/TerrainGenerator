using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class Display : MonoBehaviour
{
    public Renderer textureRenderer;


    public Texture2D DrawPerlinNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0); int height = noiseMap.GetLength(1);

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
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

        return texture;
    }

    public Texture2D DrawVoronoiMap(Color[,] colorMap)
    {
        int width = colorMap.GetLength(0); int height = colorMap.GetLength(1);
        Texture2D texture = new Texture2D(width, height);

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
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

        return texture;
    }

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;


    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.GenerateMesh();
        meshFilter.sharedMesh.subMeshCount = GetComponent<MapGenerator>().biomesAmount;

        List<int[]> subMeshList = new List<int[]>(GetComponent<MapGenerator>().biomesAmount);
        for (int i = 0; i < meshFilter.sharedMesh.tangents.Length; i = i + 3) 
        {
            
        
        }
    }
}
