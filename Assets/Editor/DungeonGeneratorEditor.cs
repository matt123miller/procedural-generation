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
                dungeonGen.CreateAllDungeon();
            }
        }

        if (GUILayout.Button("Randomise Seed"))
        {
            var rand = new System.Random();
            dungeonGen.seed = rand.Next();
            dungeonGen.CreateAllDungeon();
        }
        if (GUILayout.Button("Generate"))
        {
            dungeonGen.CreateAllDungeon();
        }
    }
}