using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dungeon
{
    public class DoorGenerator : MonoBehaviour
    {

        public List<Door> doors;

        public List<Door> Generate(List<Room> rooms)
        {
            // Sanity check/early returns
            if (rooms.Count < 2)
            {
                throw new ArgumentOutOfRangeException(
                    $"{rooms.Count} is not enough rooms to create doors between them");
            }

            // Loop the rooms from first to penultimate 
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Room currentRoom = rooms[i];
                Room nextRoom = rooms[i + 1];

                // Find shared boundary relationship as rooms usually have more than 1 door
                var relationships = FindRoomRelationships(currentRoom, nextRoom);

                // Find the vector along which that boundary exists
                
                
                // Add doors at neighbour boundaries
        

                // Add some sort of reference to each room or door, linking things together


            }


            return new List<Door>();
        }


        private Dictionary<Vector3, Room> FindRoomRelationships(Room currentRoom, Room nextRoom)
        {
            // This all feels far too verbose and wasteful.

            var currentToNext = nextRoom.neighbours.First(n => n.Value.roomID == currentRoom.roomID);
            var nextToCurrent = currentRoom.neighbours.First(n => n.Value.roomID == nextRoom.roomID);

            var relationships = new Dictionary<Vector3, Room>()
            {
                {currentToNext.Key, currentToNext.Value},
                {nextToCurrent.Key, nextToCurrent.Value}
            };

            return relationships;
        }
    }
}