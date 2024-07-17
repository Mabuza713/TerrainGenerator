using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum noiseType
    {
        Perlin,
        Biome,
        Mesh
    }
    // wich noise map to show
    public noiseType mapType;
    [Range(0.0f, 1000f)] public float meshHeightMultiplier;
    // variables used in Voronoi noise
    [Header("Usable only if map type == Biome")]
    public int biomesAmount;
    public Material[] biomeTextures;
    

    // values that defines our map
    public int width;
    public int height;
    [Range(1.0f, 100f)] public float noiseScale;
    [Range(0,10)] public int octaves;
    public float persistance;
    public float lacunarity;

    public bool Update;
    public Material vornoiMaterial;
    public Material perlinMaterial;
    private void Awake()
    {
        biomesAmount = biomeTextures.Length;
    }
    public void GenerateMap()
    {
        Display display = FindObjectOfType<Display>();
        if (mapType == noiseType.Perlin)
        {
            float[,] noiseMap = NoiseGenerator.GeneratePerlinNoiseMap(width, height, noiseScale, octaves, persistance, lacunarity);
            display.DrawPerlinNoiseMap(noiseMap);
        
        }
        else if (mapType == noiseType.Biome)
        {
            Color[,] biomeMap = NoiseGenerator.GenerateVoronoiNoiseMap(width, height, biomesAmount);
            display.DrawVoronoiMap(biomeMap);
        }
        else if (mapType == noiseType.Mesh)
        {
            
            float[,] noiseMap = NoiseGenerator.GeneratePerlinNoiseMap(width, height, noiseScale, octaves, persistance, lacunarity);
            Color[,] biomeMap = NoiseGenerator.GenerateVoronoiNoiseMap(width, height, biomesAmount);
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier), biomeMap);

            
        }

    }


}
