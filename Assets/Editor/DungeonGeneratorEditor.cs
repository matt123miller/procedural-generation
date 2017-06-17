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
                RemoveChildren(dungeonGen);
                dungeonGen.Generate();
            }
        }

        if (GUILayout.Button("Randomise Seed"))
        {
            var rand = new System.Random();
            dungeonGen.seed = rand.Next();
            RemoveChildren(dungeonGen);
            dungeonGen.Generate();
        }
        if (GUILayout.Button("Generate"))
        {
            RemoveChildren(dungeonGen);
            dungeonGen.Generate();
        }
    }

    private void RemoveChildren(DungeonGenerator generator)
    {
        List<GameObject> destroyMe = new List<GameObject>();

        var parent = generator.dungeonParent;
        for (int i = parent.childCount; i > 0; i--)
        {
            var child = parent.GetChild(0); // Safer to go back to front?
            DestroyImmediate(child.gameObject);
        }
    }
}