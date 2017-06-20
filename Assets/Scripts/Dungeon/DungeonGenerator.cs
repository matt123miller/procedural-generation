using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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
            RemoveChildren();
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
            List<Room> placedRooms = new List<Room>();

            foreach (Room room in rooms)
            {
                if (room == null)
                    continue;

                var randomPoint = Random.insideUnitCircle * mapRadius;
                    var newPos = new Vector3(randomPoint.x, 0, randomPoint.y);
                    bool overlap = placedRooms.Where(r => room.isOverlapping(r)).Any();
                    
                    if (overlap){
                        DestroyImmediate(room.gameObject);
                        Debug.Log("There was a failure!");
                        continue;
                    }
                    placedRooms.Add(room);
                    room.transform.position = newPos;
                }
            }
        }

        /// Perform flocking separation, step by step using a coroutine
        public void SeparateRooms()
        {
            foreach (var room in rooms)
            {
                if(room == null) { continue; }

                var rb = room.gameObject.AddComponent<Rigidbody>();
                var col = room.gameObject.AddComponent<BoxCollider>();
                col.size = new Vector3(room.width, 1, room.height);
                col.center = room.centre;
                rb.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionY;
                // rb.useGravity = false;
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
