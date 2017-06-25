using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Dungeon;

[CustomEditor(typeof(GraphGenerator))]
public class GraphDisplayEditor : Editor {

    void OnSceneGUI()
    {
        GraphGenerator graph = (GraphGenerator)target;
        if (graph.rooms == null) { return; }
        Handles.color = Color.white;

        Handles.DrawWireArc(graph.transform.position, Vector3.up, Vector3.forward, 360, 20);

    }
}
