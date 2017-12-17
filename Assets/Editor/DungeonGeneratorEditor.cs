using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Dungeon;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorEditor : Editor
{
    void OnSceneGUI()
    {
        DungeonGenerator dungeon = (DungeonGenerator)target;
        if (dungeon.dungeonSize == null) { return; }
        var size = dungeon.dungeonSize;
        Handles.color = Color.white;
        Vector3[] lines = { new Vector3(0, 0, 0), new Vector3(size.x, 0, 0),
            new Vector3(size.x, 0, 0), new Vector3(size.x, 0, size.y),
            new Vector3(size.x, 0, size.y), new Vector3(0, 0, size.y),
            new Vector3(0, 0, size.y), new Vector3(0, 0, 0) };
        Handles.DrawLines(lines);
        //Handles.DrawWireArc(graph.transform.position, Vector3.up, Vector3.forward, 360, 20);

    }

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

        dungeonGen.CacheReferences();

        if (GUILayout.Button("Randomise Seed"))
        {
            dungeonGen.RandomiseSeed();
        }
        if (GUILayout.Button("Generate"))
        {
            dungeonGen.Generate();
        }
    }
}