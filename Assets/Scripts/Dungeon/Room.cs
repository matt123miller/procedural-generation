using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{

    public class Room : ScriptableObject
    {
        public int width, height;
        [MinMaxRange(4, 10)]
        public RangedFloat widthRange;
        [MinMaxRange(4, 10)]
        public RangedFloat heightRange;

        public Transform[,] tiles;

        private void OnEnable()
        {
            width = (int)Random.Range(widthRange.minValue, widthRange.maxValue);
            height = (int)Random.Range(heightRange.minValue, heightRange.maxValue);
        }

        public GameObject[,] GenerateTiles()
        {
            GameObject[,] tiles = new GameObject[width, height];

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    tile.transform.position = new Vector2(w, h);
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
