using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Dungeon;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DungeonGenerator dungeonGen = (DungeonGenerator)target;

        if (DrawDefaultInspector())
        {
            if (dungeonGen.autoUpdate)
            {
                dungeonGen.Generate();
            }
        }

        if (GUILayout.Button("Randomise Seed"))
        {
            var rand = new System.Random();
            dungeonGen.seed = rand.Next();
            dungeonGen.Generate();
        }
        if (GUILayout.Button("Generate"))
        {
            dungeonGen.Generate();
        }
    }
}