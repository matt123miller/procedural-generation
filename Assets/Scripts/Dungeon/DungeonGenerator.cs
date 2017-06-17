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

        public LayerMask wallMask, floorMask;
        public Material[] materials;
        public int totalRooms = 10;
        public List<Room> rooms = new List<Room>();

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
                var mat = materials[Random.Range(0, materials.Length)];

                var room = parent.AddComponent<Room>();
                room.SetupRoom(w, h, mat);
                rooms.Add(room);
            }
        }
    }
}
