using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // values that defines our map
    public int width;
    public int height;
    public float noiseScale;

    public void GenerateMap()
    {
        float[,] noiseMap = NoiseGenerator.GenerateNoiseMap(width, height, noiseScale);

        Display display = FindObjectOfType<Display>();
        display.DrawNoiseMap(noiseMap);
    }


}
