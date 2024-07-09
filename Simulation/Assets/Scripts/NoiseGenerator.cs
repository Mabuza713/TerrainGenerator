using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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


    public static float[,] GenerateVoronoiNoiseMap(int width, int height, int biomesAmount)
    {
        Vector2Int[] centroids = new Vector2Int[biomesAmount];
        Color[] regions = new Color[biomesAmount]; // might change for textrures[] ???
        for (int i = 0; i < biomesAmount; i++)
        {
            centroids[i] = new Vector2Int(Random.Range(0,width), Random.Range(i,height));
            regions[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f); // might need to change to pick random texture from previously created ??
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

            }
        }
        float[,] biomeMap = new float[width,height];
        return biomeMap;
    



        int GetCentroidIndex(Vector2Int pixelPosition)
        {
            float minDistance = float.PositiveInfinity;
            int index = 0;
            for (int j = 0; j < biomesAmount; j++)
            {
                if (Vector2.Distance(centroids[j], pixelPosition) < minDistance)
                {
                    index = j;
                }
            }

            
            
            return index;
        }
    }
}
