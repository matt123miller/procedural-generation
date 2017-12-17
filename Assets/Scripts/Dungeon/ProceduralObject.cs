using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class ProceduralObject : MonoBehaviour
    {
        
        protected GameObject CreateWall(int x, int z)
        {
            var wall = CreatePrimitive(x, z, PrimitiveType.Cube);
            wall.transform.Translate(0, 0.5f, 0);
            wall.layer = Layers.Wall;
            wall.name = "Wall";
            return wall;
        }

        protected GameObject CreateDoor(int x, int z)
        {
            var tile = CreatePrimitive(x, z, PrimitiveType.Quad);
            tile.transform.Rotate(90, 0, 0);
            tile.layer = Layers.Door;
            tile.name = "Door";
            return tile;
        }

        protected GameObject CreateFloor(int x, int z)
        {
            var tile = CreatePrimitive(x, z, PrimitiveType.Quad);
            tile.transform.Rotate(90, 0, 0);
            tile.layer = Layers.Floor;
            tile.name = "Floor";
            return tile;
        }

        protected GameObject CreatePrimitive(int x, int z, PrimitiveType type)
        {
            var primitive = GameObject.CreatePrimitive(type);
            primitive.transform.position = new Vector3(x, 0, z);
            primitive.transform.SetParent(transform);
            return primitive;
        }

    }
}
