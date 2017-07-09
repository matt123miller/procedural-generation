using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.Generate();
            }
        }

        if (GUILayout.Button("Randomise Seed"))
        {
            mapGen.RandomiseSeed();
            mapGen.Generate();
        }
        if (GUILayout.Button ("Generate")) {
			mapGen.Generate();
		}
    }
}