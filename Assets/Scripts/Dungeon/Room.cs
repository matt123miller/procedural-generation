using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{

    public class Room : MonoBehaviour
    {
        public int width, height;

        public Transform[,] tiles;

        public void SetupRoom(int _width, int _height)
        {
            width = _width;
            height = _height;
        }

        public GameObject[,] GenerateTiles()
        {
            GameObject[,] tiles = new GameObject[width, height];

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    tile.transform.position = new Vector3(w, 0, h);
                    tiles[w, h] = tile;
                }
            }

            return tiles;
        }

        public GameObject[] GenerateWalls()
        {
            GameObject[] walls = new GameObject[width * 2 + height * 2 + 4];

            return walls;
        }
    }
}
