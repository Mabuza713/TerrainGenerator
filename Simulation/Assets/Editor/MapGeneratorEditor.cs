using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;


        if (DrawDefaultInspector())
        {
            if (mapGen.Update)
            {
                mapGen.GenerateMap();
            }
        }

        if (GUILayout.Button("GENERATE"))
        {
            mapGen.GenerateMap();
        }
    }
}
