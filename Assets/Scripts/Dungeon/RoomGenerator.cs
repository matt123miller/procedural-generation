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

        public Material[] materials;
        public int roomPlacementAttempts = 10;
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

                // Place the room
                if (i == 0)
                { // The first room should start at 0, makes certain things easier.
                    room.transform.position = Vector3.zero;
                }
                else
                {
                    // Pick a side of the previous room.
                    Room prevRoom = rooms[rooms.Count() - 1];
                    Vector3[] sides = { prevRoom.Centre.plusZ(prevRoom.Top/2), prevRoom.Centre.plusX(prevRoom.Right/2),
                                        prevRoom.Centre.plusZ(-prevRoom.Bottom/2), prevRoom.Centre.plusX(-prevRoom.Left/2) };

                    int edgeIndex = Random.Range(0, sides.Length);
                    Vector3 edge = sides[edgeIndex];

                    // What goes here? If we're moving left or down (negative numbers) then how does that change things?
                    // Maybe it should be < 1 as positive changes actually require a weird offset. 
                    var offset = edgeIndex > 1 ? 0 : 0;

                    //room.Centre = edge + new Vector3(room.width / 2, 0, room.height / 2);
                    room.SetPosition(prevRoom.transform.position.plusX(prevRoom.width));
                }

                // Is that space occupied somehow?
                //bool overlap = rooms.Any(r => room.IsOverlapping(r));

                // If it is then lets destroy this room and move onto another
                //if (overlap)
                //{
                //    print("Failure");
                //    DestroyImmediate(room.gameObject);
                //    continue;
                //}

                // Otherwise, save the room to the list.
                rooms.Add(room);
                successes++;
            }

            // No longer needed
            //print(rooms.Count());
            //rooms = rooms.Where(r => r != null).ToList();
            //print(rooms.Count());

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