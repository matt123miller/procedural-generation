using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dungeon
{
    public class DoorGenerator : MonoBehaviour, IEmptyable
    {
        private readonly Vector3[] directions = new Vector3().CardinalDirections();

        public Transform doorParent;
        public List<Door> doors;

        public async Task<List<Door>> Generate( List<Room> rooms )
        {
            EmptyContents( false );
            doors = new List<Door>();

            // Sanity check/early returns
            if (rooms.Count < 2)
            {
                var failMessage = $"{rooms.Count} is not enough rooms to create doors between them";
                throw new ArgumentOutOfRangeException( failMessage );
            }

            // Don't need the variables out here, it's just easier to debug.
            RoomTransitionRelationship currentRelationship;
            Vector3 currentDoorPosition;
            Door currentDoor;

            // could use rooms.Select if I was feeling fancy
            // Loop the rooms from first to penultimate 
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Room currentRoom = rooms[i];
                Room nextRoom = rooms[i + 1];

                // Find the correct shared boundary relationship as rooms usually have more than 1 door
                currentRelationship = FindRoomRelationship( currentRoom, nextRoom );
                // Find a position along the shared boundary to place a door at
                currentDoorPosition = FindDoorPositionAlongBoundary( currentRelationship );
                // Add doors at neighbour boundaries
                currentDoor = CreateDoor( currentDoorPosition );
                // Add some sort of reference to each room or door, linking things together. Could be handled in CreateDoor?
                currentDoor.CacheNeighbours( currentRelationship );

                RemoveWallsForDoor( currentRelationship, currentDoor );

                doors.Add( currentDoor );
            }

            return doors;
        }

        private void RemoveWallsForDoor( RoomTransitionRelationship currentRelationship, Door door )
        {
            // loop both rooms walls, delete the walls in each that overlap with the door
            var walls1 = currentRelationship.from.walls;
            var walls2 = currentRelationship.to.walls;

            var target1 = walls1.First(w => ProceduralObject.IsOverlapping(w.position, door.transform.position));
            var target2 = walls2.First(w => ProceduralObject.IsOverlapping(w.position, door.transform.position));

            print("These 2 walls should be the same");
            print($"wall 1 -> {target1}, wall 2 -> {target2}");

            DestroyImmediate(target1.gameObject);
            DestroyImmediate(target2.gameObject);
        }

        private RoomTransitionRelationship FindRoomRelationship( Room currentRoom, Room nextRoom )
        {
            // This method could be made to use Tuple<Room, Room, Vector3> instead but I don't like the syntax.
            // So I'm gonna lean on some DSL struct instead. Kind of like passing objects around in JS or Python

            var currentToNext = currentRoom.neighbours.First( n => n.Value.roomID == nextRoom.roomID );

            return new RoomTransitionRelationship( currentRoom, nextRoom, currentToNext.Key );
        }


        private Vector3 FindDoorPositionAlongBoundary( RoomTransitionRelationship relationship )
        {
            /**
             *
             * If they share a top/bottom boundary then....
             * See my notebook for details.
             * Too tired tonight
             * 
             * This could be generalised to avoid the 2 almost identical if branches. But not right now.
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

                int maxLeft = (int)Math.Max( from.Left, to.Left );
                int minLeft = (int)Math.Min( from.Left, to.Left );

                int maxRight = (int)Math.Max( from.Right, to.Right );
                int minRight = (int)Math.Min( from.Right, to.Right );

                minBound = minLeft + (maxLeft - minLeft) + 1;
                maxBound = maxRight - (maxRight - minRight) - 1;

                int valueAlongBoundary = RandomValueAlongBoundary( minBound, maxBound );

                frozenEdgeValue = viaDirection == Vector3.forward ? (int)from.Top + 1 : (int)from.Bottom;

                chosenPosition = new Vector3( valueAlongBoundary, 0, frozenEdgeValue );
            }
            else if (viaDirection == Vector3.left || viaDirection == Vector3.right)
            {
                /**
                 * The rooms share a right/left edge and will have a door between them via this edge.
                 * Therefor the x value will be from that edge and the y value between the
                 * min and max values of the y intersection
                 */

                int maxTop = (int)Math.Max( from.Top, to.Top );
                int minTop = (int)Math.Min( from.Top, to.Top );

                int maxBottom = (int)Math.Max( from.Bottom, to.Bottom );
                int minBottom = (int)Math.Min( from.Bottom, to.Bottom );

                maxBound = maxTop - (maxTop - minTop) - 1;
                minBound = minBottom + (maxBottom - minBottom) + 1;

                // Call a function to get a random position along our computed boundary area.
                int valueAlongBoundary = RandomValueAlongBoundary( minBound, maxBound );

                // from.right + 1 is to offset 1 space so doors are correctly between rooms
                frozenEdgeValue = viaDirection == Vector3.left ? (int)from.Left : (int)from.Right + 1;

                chosenPosition = new Vector3( frozenEdgeValue, 0, valueAlongBoundary );
            }

            // Call a function to get a random position along our computed boundary area.

            return chosenPosition;
        }

        private int RandomValueAlongBoundary( int minBound, int maxBound )
        {
            // Create a function in case I need to do some + / - stuff to get it within walls.
            return UnityEngine.Random.Range( minBound, maxBound );
        }


        // Should this really be here?
        private Door CreateDoor( Vector3 doorPosition )
        {
            var doorResource = Resources.Load( "Prefabs/Door", typeof( GameObject ) );
            var doorGo = Instantiate( doorResource, doorParent, true ) as GameObject;
            var door = doorGo.GetComponent<Door>();
            door.InitialiseWithData( doorPosition );

            return door;
        }

        public void EmptyContents( bool purgeList )
        {
            for (int i = doorParent.childCount; i > 0; i--) // Safer to go back to front?
            {
                var child = doorParent.GetChild( 0 );
                DestroyImmediate( child.gameObject );
            }

            if (purgeList)
            {
                doors = new List<Door>();


            }
        }
    }
}