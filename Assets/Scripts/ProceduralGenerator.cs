using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ProceduralGenerator : MonoBehaviour {

    public int seed;
    public bool autoUpdate;
    public bool autoBuildAtStart = false;

    public abstract void CacheReferences();
    public abstract void Generate();

    public void RandomiseSeed()
    {
        var rand = new System.Random();
        seed = rand.Next();
    }
}
