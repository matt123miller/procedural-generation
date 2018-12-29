using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dungeon
{
    public class DoorGenerator : MonoBehaviour
    {
        private readonly Vector3[] directions = new Vector3().CardinalDirections();

        public List<Door> doors;

        public List<Door> Generate(List<Room> rooms)
        {
            // Sanity check/early returns
            if (rooms.Count < 2)
            {
                var failMessage = $"{rooms.Count} is not enough rooms to create doors between them";
                throw new ArgumentOutOfRangeException(failMessage);
            }

            // Loop the rooms from first to penultimate 
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Room currentRoom = rooms[i];
                Room nextRoom = rooms[i + 1];

                // Find the correct shared boundary relationship as rooms usually have more than 1 door
                var relationship = FindRoomRelationship(currentRoom, nextRoom);

                // Find a position along the shared boundary to place a door at
                var boundary = FindDoorPositionAlongBoundary(relationships);
                
                // Add doors at neighbour boundaries
        

                // Add some sort of reference to each room or door, linking things together


            }


            return new List<Door>();
        }

        private RoomTransitionRelationship FindRoomRelationship(Room currentRoom, Room nextRoom)
        {
            // This method could be made to use Tuple<Room, Room, Vector3> instead but I don't like the syntax.
            // So I'm gonna lean on some DSL struct instead. Kind of like passing objects around in JS or Python

            var currentToNext = nextRoom.neighbours.First(n => n.Value.roomID == currentRoom.roomID);

            return new RoomTransitionRelationship(currentRoom, nextRoom, currentToNext.Key);
        }

            
            /**
             *
             * If they share a top/bottom boundary then....
             * See my notebook for details.
             * Too tired tonight
             * 
             */
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