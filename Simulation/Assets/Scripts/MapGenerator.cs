using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum noiseType
    {
        Perlin,
        Biome
    }
    // wich noise map to show
    public noiseType mapType;

    // variables used in Voronoi noise
    [Header("Usable only if map type == Biome")]
    public int biomesAmount;
    
    
    // values that defines our map
    public int width;
    public int height;
    [Range(1.0f, 100f)] public float noiseScale;
    [Range(0,10)] public int octaves;
    public float persistance;
    public float lacunarity;

    public bool Update;
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

    }


}
