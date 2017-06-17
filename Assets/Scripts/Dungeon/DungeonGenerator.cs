using UnityEngine;
using System.Collections.Generic;

namespace Dungeon
{
    public class DungeonGenerator : ProceduralGenerator
    {
        [Range(30, 100)] public int mapRadius;
        [Range(10, 18)] public int minWidth;
        [Range(18, 30)] public int maxWidth;
        [Range(10, 18)] public int minHeight;
        [Range(18, 30)] public int maxHeight;

        public Transform dungeonParent;

        public Material[] materials;
        public int totalRooms = 10;
        public List<Room> rooms = new List<Room>();

        private void Awake()
        {
            Generate();
        }

        public override void Generate()
        {
            CreateRooms();
            PlaceRooms();
            SeparateRooms();
        }

        private void CreateRooms()
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

        public void PlaceRooms()
        {
            foreach (Room room in rooms)
            {
                var randomPoint = Random.insideUnitCircle * mapRadius;
                if (room)
                    room.transform.position = new Vector3(randomPoint.x, 0, randomPoint.y);
            }
        }

        public void SeparateRooms()
        {

        }
    }
}
