using UnityEngine;
using System.Collections.Generic;

namespace Dungeon
{
    public class DungeonGenerator : ProceduralGenerator
    {
        [Range(5, 12)] public int minWidth;
        [Range(12, 20)] public int maxWidth;
        [Range(5, 12)] public int minHeight;
        [Range(12, 20)] public int maxHeight;

        public Transform dungeonParent;

        public int totalRooms = 10;

        private void Awake()
        {
            Generate();
        }

        public override void Generate()
        {
            for (int i = 0; i < totalRooms; i++)
            {
                var parent = new GameObject();
                parent.name = "Room " + i;
                parent.transform.SetParent(dungeonParent);

                int w = Random.Range(minWidth, maxWidth);
                int h = Random.Range(minHeight, maxHeight);

                var room = parent.AddComponent<Room>();
                room.SetupRoom(w, h);
                var tiles = room.GenerateTiles();
                var walls = room.GenerateWalls();

                print(tiles);
                foreach (var tile in tiles)
                {
                    tile.transform.SetParent(parent.transform);
                }
            }
        }
    }
}
