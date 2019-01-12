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

        public Transform doorParent;
        public List<Door> doors;

        public List<Door> Generate(List<Room> rooms)
        {
            doors = new List<Door>();
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
                var doorPosition = FindDoorPositionAlongBoundary(relationship);
                // Add doors at neighbour boundaries
                var door = CreateDoor(doorPosition);
                // Add some sort of reference to each room or door, linking things together
                door.CacheNeighbours(relationship);
                
                doors.Add(door);            
            }


            return doors;
        }

        private RoomTransitionRelationship FindRoomRelationship(Room currentRoom, Room nextRoom)
        {
            // This method could be made to use Tuple<Room, Room, Vector3> instead but I don't like the syntax.
            // So I'm gonna lean on some DSL struct instead. Kind of like passing objects around in JS or Python
            
            var currentToNext = currentRoom.neighbours.First(n => n.Value.roomID == nextRoom.roomID);

            return new RoomTransitionRelationship(currentRoom, nextRoom, currentToNext.Key);
        }

        
        private Vector3 FindDoorPositionAlongBoundary(RoomTransitionRelationship relationship)
        {   
            /**
             *
             * If they share a top/bottom boundary then....
             * See my notebook for details.
             * Too tired tonight
             * 
             */
            var from = relationship.from;
            var to = relationship.to;
            var viaDirection = relationship.via;

            int maxBound, minBound, frozenEdgeValue;
            var chosenPosition = new Vector3();
            
            if (viaDirection == Vector3.forward || viaDirection == Vector3.back)
            {
                /**
                 * The rooms share a left/right edge and will have a door between them via this edge.
                 * Therefor the y value will be from that edge and the x value between the
                 * min and max values of the x intersection
                 */
                 
                int maxLeft = (int)Math.Max(from.Left, to.Left);
                int minLeft = (int)Math.Min(from.Left, to.Left);

                int maxRight = (int)Math.Max(from.Right, to.Right);
                int minRight = (int)Math.Min(from.Right, to.Right);

                minBound = minLeft + (maxLeft - minLeft) + 1;
                maxBound = maxRight - (maxRight - minRight) - 1;

                int valueAlongBoundary = RandomValueAlongBoundary(minBound, maxBound);

                frozenEdgeValue = viaDirection == Vector3.forward ? (int) from.Top + 1 : (int) from.Bottom;
                
                chosenPosition = new Vector3(valueAlongBoundary, 0, frozenEdgeValue);
            }
            else if (viaDirection == Vector3.left || viaDirection == Vector3.right)
            {
                /**
                 * The rooms share a right/left edge and will have a door between them via this edge.
                 * Therefor the x value will be from that edge and the y value between the
                 * min and max values of the y intersection
                 */
                
                int maxTop = (int)Math.Max(from.Top, to.Top);
                int minTop = (int)Math.Min(from.Top, to.Top);

                int maxBottom = (int)Math.Max(from.Bottom, to.Bottom);
                int minBottom = (int)Math.Min(from.Bottom, to.Bottom);

                maxBound = maxTop - (maxTop - minTop) - 1;
                minBound = minBottom + (maxBottom - minBottom) + 1;
                
                // Call a function to get a random position along our computed boundary area.
                int valueAlongBoundary = RandomValueAlongBoundary(minBound, maxBound);
                
                // from.right + 1 is to offset 1 space so doors are correctly between rooms
                frozenEdgeValue = viaDirection == Vector3.left ? (int) from.Left : (int) from.Right + 1 ;

                chosenPosition = new Vector3(frozenEdgeValue, 0, valueAlongBoundary);
            }
            
            // Call a function to get a random position along our computed boundary area.
            
            return chosenPosition;
        }

        private int RandomValueAlongBoundary(int minBound, int maxBound)
        {
            // Create a function in case I need to do some + / - stuff to get it within walls.
            return Random.Range(minBound, maxBound);
        }
        
        
        // Should this really be here?
        private Door CreateDoor(Vector3 doorPosition)
        {
            var doorResource = Resources.Load("Prefabs/Door", typeof(GameObject));
            var doorGo = Instantiate(doorResource, doorParent, true) as GameObject;
            var door = doorGo.GetComponent<Door>();
            door.InitialiseWithData(doorPosition);

            return door;
        }

    }
}