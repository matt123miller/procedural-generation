using UnityEngine;

namespace Dungeon
{
    class DungeonGenerator : MonoBehaviour
    {
        public int totalRooms = 10;

        private void Awake()
        {
            GenerateDungeon();
        }

        private void GenerateDungeon()
        {
            for (int i = 0; i < totalRooms; i++)
            {
                var parent = new GameObject();
                parent.transform.SetParent(transform);

                var room = ScriptableObject.CreateInstance<Room>();
                var tiles = room.GenerateTiles();
                var walls = room.GenerateWalls();
                

            }
        }
    }
}
