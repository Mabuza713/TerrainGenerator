using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator 
{
    public static float[,] GenerateNoiseMap(int width, int height, float scale) 
    {
        float offsetX = Random.Range(0, 9999f);
        float offsetY = Random.Range(0, 9999f);

        float[,] noiseMap = new float[width, height];
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sampleX =(float) x / width * scale + offsetX;
                float sampleY =(float) y / height * scale + offsetY;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }
    
        return noiseMap;
    }


}
