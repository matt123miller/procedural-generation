using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Dungeon
{
    enum DungeonObjects
    {
        EMPTY = 0,
        WALL = 1,
        FLOOR = 2,
        DOOR = 3
    }
    public class DungeonGenerator : ProceduralGenerator
    {
        [Range(40, 60)] public int minWidth;
        [Range(60, 120)] public int maxWidth;
        [Range(40, 60)] public int minHeight;
        [Range(60, 120)] public int maxHeight;

        public Vector2 dungeonSize;

        public int[,] grid;

        public RoomGenerator roomGen;
        public CorridorGenerator corridorGen;
        private void Awake()
        {
            Generate();
        }

        public override void Generate()
        {
            GenerateDungeon();

            roomGen = GetComponent<RoomGenerator>();
            corridorGen = GetComponent<CorridorGenerator>();

            var rooms = roomGen.Generate(dungeonSize);

            UpdateGridWithRooms(roomGen.rooms);
            print(this);
        }

        private void GenerateDungeon()
        {
            int w = Random.Range(minWidth, maxWidth);
            int h = Random.Range(minHeight, maxHeight);
            dungeonSize = new Vector2(w, h);
            grid = new int[w, h];
        }


        private void UpdateGridWithRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                if (room == null) { continue; }
                int rootX = (int)room.transform.position.x;
                int rootY = (int)room.transform.position.z;
                
                for (int x = 0; x < room.width; x++)
                {
                    for (int y = 0; y < room.height; y++)
                    {
                        bool edge = (x == 0 || y == 0 || x == room.width - 1 || y == room.height - 1);
                      
                        grid[rootX + x, rootY + y] = edge ? (int)DungeonObjects.WALL : (int)DungeonObjects.FLOOR;
                    }
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("This dungeon:\n");
            sb.Append("Has " + roomGen.rooms.Count() + " Rooms\n");
            sb.Append("Has the bounds " + dungeonSize + "\n");

            for (int y = (int)dungeonSize.y - 1; y >= 0; y--)
            {
                for (int x = 0; x < dungeonSize.x; x++)
                {
                    sb.Append(grid[x, y]);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
