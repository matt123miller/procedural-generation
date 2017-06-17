using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralGenerator : MonoBehaviour {

    public int seed;
    public bool autoUpdate;

    public abstract void Generate();
}
