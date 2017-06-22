using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Dungeon
{
    public class DungeonGenerator : ProceduralGenerator
    {
        [Range(40, 60)] public int minWidth;
        [Range(60, 120)] public int maxWidth;
        [Range(40, 60)] public int minHeight;
        [Range(60, 120)] public int maxHeight;

        public Vector2 dungeonSize;
        
        public Transform[,] grid;

        public RoomGenerator roomGen;
        private void Awake()
        {
            Generate();
        }

        public override void Generate()
        {
            GenerateDungeon();

            roomGen = GetComponent<RoomGenerator>();
            var rooms = roomGen.Generate(dungeonSize);

            UpdateGridWithRooms(roomGen.rooms);
        }

        private void GenerateDungeon()
        {
            int w = Random.Range(minWidth, maxWidth);
            int h = Random.Range(minHeight, maxHeight);
            dungeonSize = new Vector2(w, h);
            grid = new Transform[w, h];
        }


        private void UpdateGridWithRooms(Room[] rooms)
        {
            foreach (Room room in rooms)
            {
                if (room == null) { continue; }

                for (int i = 0; i < room.transform.childCount; i++)
                {
                    var child = room.transform.GetChild(i);

                    grid[(int)child.transform.position.x, (int)child.transform.position.z] = child;
                }
            }
        }
    }
}
