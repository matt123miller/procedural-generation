using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Layers{

    public static readonly int Wall = LayerMask.NameToLayer("Wall");
    public static readonly int Door = LayerMask.NameToLayer("Door");
    public static readonly int Floor = LayerMask.NameToLayer("Floor");
    public static readonly int Enemy = LayerMask.NameToLayer("Enemy");
    public static readonly int Friendly = LayerMask.NameToLayer("Friendly");
    public static readonly int Obstacle = LayerMask.NameToLayer("Obstacle");

    //public static int IntFromLayerMask
}
