using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dungeon
{
    public class RoomGenerator : MonoBehaviour
    {

        [Range(10, 18)] public int minWidth;
        [Range(18, 30)] public int maxWidth;
        [Range(10, 18)] public int minHeight;
        [Range(18, 30)] public int maxHeight;
        public Transform dungeonParent;

        public Material[] materials;
        public int roomPlacementAttempts = 10;
        public Room[] rooms;

        public Room[] Generate(Vector2 dungeonSize)
        {
			RemoveChildren();
			rooms = new Room[roomPlacementAttempts];
			CreateRooms(dungeonSize);
			// TODO: Try and clear the remaining empty room spaces.
			return rooms;
        }
		
        private void CreateRooms(Vector2 dungeonSize)
        {
			int successes = 0;
            for (int i = 0; i < roomPlacementAttempts; i++)
            {
                var parent = new GameObject();
                parent.name = "Room " + i;
                parent.transform.SetParent(dungeonParent);

                int w = Random.Range(minWidth, maxWidth);
                int h = Random.Range(minHeight, maxHeight);
                var mat = materials[Random.Range(0, materials.Length)];

                var room = parent.AddComponent<Room>();
                room.SetupRoom(w, h, mat);

                // Place the room
                room.PlaceRandomly(dungeonSize, rooms);

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
                rooms[successes] = room;
				successes++;
            }

        }



        /// Perform flocking separation, step by step using a coroutine
        public void SeparateRooms()
        {
            foreach (var room in rooms)
            {
                if (room == null) { continue; }

                var rb = room.gameObject.AddComponent<Rigidbody>();
                var col = room.gameObject.AddComponent<BoxCollider>();
                col.size = new Vector3(room.width, 1, room.height);
                col.center = room.centre;
                rb.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionY;
                rb.useGravity = false;
            }
            // StartCoroutine(Separate());
        }

        IEnumerator Separate()
        {
            while (true)
            {
                Debug.Log("Hi");
                yield return new WaitForFixedUpdate();
            }
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