using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dungeon
{
    public class RoomGenerator : MonoBehaviour
    {

        [Range(10, 18)] public int minWidth;
        [Range(18, 30)] public int maxWidth;
        [Range(10, 18)] public int minHeight;
        [Range(18, 30)] public int maxHeight;
        private Vector2 dungeonSize;
        public Transform dungeonParent;
        [Range(0, 5)] public int optionalRoomLoopbacks = 0;
        public int roomPlacementAttempts = 10;
        public Material[] materials;
        private Vector3[] directions = new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };

        public List<Room> rooms;

        public List<Room> Generate(Vector2 _dungeonSize)
        {
            RemoveChildren();
            rooms = new List<Room>();
            dungeonSize = _dungeonSize;
            CreateRooms(_dungeonSize);
            // TODO: Try and clear the remaining empty room spaces.

            return rooms;
        }

        private void CreateRooms(Vector2 dungeonSize)
        {
            int successes = 0;

            directions.ForEach(d => print(d));

            for (int i = 0; i < roomPlacementAttempts; i++)
            {
                var parent = new GameObject();
                parent.name = "Room " + successes;
                parent.transform.SetParent(dungeonParent);

                int w = Random.Range(minWidth, maxWidth);
                int h = Random.Range(minHeight, maxHeight);
                var mat = materials[Random.Range(0, materials.Length)];

                var room = parent.AddComponent<Room>();
                room.SetupRoom(w, h, mat);

                // Where to place the room?

                if (i == 0)
                { // The first room should start at 0, makes certain things easier.
                    room.transform.position = Vector3.zero;
                    rooms.Add(room);
                    successes++;
                    continue;
                }

                int prevIndexMod = successes - 1;
                // Should we loop back to a previous room and try to place from there?
                // This will be reused later
                bool loopback = optionalRoomLoopbacks != 0 && successes % optionalRoomLoopbacks == 0;
                if (loopback)
                {
                    prevIndexMod = successes - Random.Range(1, optionalRoomLoopbacks + 1);
                }

                Room prevRoom = rooms[prevIndexMod];

                room.transform.position = PlaceRoom(room, prevRoom); ;


                // Is that space occupied somehow?
                bool overlap = rooms.Any(r => room.IsOverlapping(r));

                // If it is then lets destroy this room and move onto another
                if (overlap)
                {
                    print("Failure");
                    DestroyImmediate(room.gameObject);
                    continue;
                }

                // Otherwise, save the room to the list.
                rooms.Add(room);
                successes++;
            }

            // No longer needed
            //print(rooms.Count());
            //rooms = rooms.Where(r => r != null).ToList();
            //print(rooms.Count());

        }

        private Vector3 PlaceRoom(Room room, Room prevRoom)
        {
            Vector3 target;
            // Conditionals are life.
            int dx = Random.Range(0, 2) == 1 ? 1 : -1;
            int dy = Random.Range(0, 2) == 1 ? 1 : -1;

            // Pick a direction, north south east or west
            Vector3 direction = directions.random();

            // Need to offset when going left or down, e.g. offset by prevRoom - room
            int widthOffset = direction.x == -1 ? (prevRoom.width - room.width) : 0;
            int heightOffset = direction.z == -1 ? (prevRoom.height - room.height) : 0;
            var edgeOffset = new Vector3(prevRoom.width * direction.x + widthOffset, 0, prevRoom.height * direction.z + heightOffset);

            // Make some random offest along the chosen edge, makes it look less square.
            int vx = Random.Range(1, room.floorWidth / 2);
            int vz = Random.Range(1, room.floorHeight / 2);
            // Flipping the direction z and x multiplication here neatly allows me to switch from moving towards an edge to moving along it
            // It's just easy to miss so watch out for that.
            var variationOffset = new Vector3(vx * direction.z, 0, vz * direction.x);

            target = prevRoom.transform.position + edgeOffset + variationOffset;
            // Anything else that needs to happen to target?
            return target;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("There are " + rooms.Count() + " rooms");
            for (int x = 0; x < dungeonSize.x; x++)
            {
                for (int y = 0; y < dungeonSize.y; y++)
                {

                }
            }
            return sb.ToString();
        }

        private void RemoveChildren()
        {
            for (int i = dungeonParent.childCount; i > 0; i--) // Safer to go back to front?
            {
                var child = dungeonParent.GetChild(0);
                DestroyImmediate(child.gameObject);
            }
        }
    }
}