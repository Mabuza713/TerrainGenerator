using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public static class NoiseGenerator 
{
    public static float[,] GeneratePerlinNoiseMap(int width, int height, float scale, int octaves, float persistance, float lacunarity) 
    {
        Random.seed = 32;
        float offsetX = Random.Range(0, 9999f);
        float offsetY = Random.Range(0, 9999f);
        float maxHeight = float.NegativeInfinity;
        float minHeight = float.PositiveInfinity;
        float[,] noiseMap = new float[width, height];
        if (scale <= 0)
        {
            scale = 0.0001f;
        }


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - width/2) / scale * frequency + offsetX;
                    float sampleY = (y - height/2)/ scale * frequency + offsetY;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                    
                }
                if (noiseHeight > maxHeight)
                {
                    maxHeight = noiseHeight;
                }
                if (noiseHeight < minHeight)
                {
                    minHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseMap[x,y] = Mathf.InverseLerp(minHeight, maxHeight, noiseMap[x,y]);
            }
        }

        return noiseMap;
    }


    public static Color[,] GenerateVoronoiNoiseMap(int width, int height, int biomesAmount)
    {
        Vector2Int[] centroids = new Vector2Int[biomesAmount];
        Color[] biomes = new Color[biomesAmount]; // might change for textrures[] ???
        for (int i = 0; i < biomesAmount; i++)
        {
            centroids[i] = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            biomes[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f); // might need to change to pick random texture from previously created ??
        }

        Color[,] colorMap = new Color[width , height];
        float[] distances = new float[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[x, y] = biomes[GetCentroidIndex(new Vector2Int(x, y))];
                distances[x * width + y] = Vector2.Distance(new Vector2Int(x, y), centroids[GetCentroidIndex(new Vector2Int(x, y))]);
            }
        }

        float maxDistance = GetMaxDistance(distances);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[x, y] = colorMap[x, y];
            }
        }

        return colorMap;
        int GetCentroidIndex(Vector2Int pixelPosition)
        {
            float minDistance = float.PositiveInfinity;
            int index = 0;
            for (int j = 0; j < biomesAmount; j++)
            {
                if (Vector2.Distance(centroids[j], pixelPosition) < minDistance)
                {
                    index = j;
                    minDistance = Vector2.Distance(centroids[j], pixelPosition);
                }
            }
            return index;
        }
        float GetMaxDistance(float[] distances)
        {
            float maxDistance = float.NegativeInfinity;
            foreach (float distance in distances)
            {
                if (distance > maxDistance) { maxDistance = distance; }
            }
            return maxDistance;
        }
    }
}
