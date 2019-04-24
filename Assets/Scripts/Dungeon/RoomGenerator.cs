using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Dungeon
{
    public class RoomGenerator : MonoBehaviour, IEmptyable
    {
        [Range( 10, 18 )] public int minWidth;
        [Range( 18, 30 )] public int maxWidth;
        [Range( 10, 18 )] public int minHeight;
        [Range( 18, 30 )] public int maxHeight;
        public bool stepThrough = true;
        private Vector2 dungeonSize;
        public Transform roomParent;
        [Range( 0, 5 )] public int optionalRoomLoopbacks = 0;
        [Range( 0, 20 )] public int corridorChance = 5;
        public int roomPlacementAttempts = 10;
        public Material[] materials;
        private readonly Vector3[] directions = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };

        public List<Room> rooms;

        public async Task<List<Room>> Generate( Vector2 _dungeonSize )
        {
            dungeonSize = _dungeonSize;
            EmptyContents( false );
            rooms = new List<Room>();

            //var createRoomsRoutine = CreateRooms(dungeonSize);
//#if UNITY_EDITOR
//            if (EditorApplication.isPlaying)
//            {
                await CreateRooms( dungeonSize );
//            }
//#endif
            return rooms;
        }

        private async Task CreateRooms( Vector2 dungeonSize )
        {
            int successes = 0;

            for (int i = 0; i < roomPlacementAttempts; i++)
            {
                var parent = new GameObject();
                parent.name = "Room " + successes;
                parent.transform.SetParent( roomParent );

                int w = Random.Range( minWidth, maxWidth );
                int h = Random.Range( minHeight, maxHeight );
                var mat = materials.random();

                // Corridors are still a type of room, just with a different data initialisation
                Room room = Random.Range( 0, 100 ) > corridorChance ?
                                                    parent.AddComponent<Room>() :
                                                    parent.AddComponent<Corridor>();

                room.InitialiseWithData( i, w, h, mat );

                // Where to place the room?

                if (i == 0)
                {
                    // The first room should start at 0, makes certain things easier.
                    room.transform.position = Vector3.zero;
                    rooms.Add( room );
                    room.Create();
                    successes++;
                    continue;
                }

                int prevIndexMod = successes - 1;

                // TODO: Somehow make this recursive instead of a weird loopback thing?
                // This will be reused later
                bool loopback = optionalRoomLoopbacks != 0 && (successes % optionalRoomLoopbacks + 2) == 0;
                if (loopback)
                {
                    // Should we loop back to a previous room and try to place from there?
                    prevIndexMod = successes - Random.Range( 1, optionalRoomLoopbacks + 1 );
                    prevIndexMod = Mathf.Min( 0, prevIndexMod );
                }


                Room adjoiningRoom = rooms[prevIndexMod];

                Vector3 direction = ChooseRoomDirection( room );

                Vector3 chosenRootPosition = DecideRoomPlacement( room, adjoiningRoom, direction );

                room.SetPosition( chosenRootPosition );

                // Is that space occupied somehow?
                bool overlap = rooms.Any( r => room.IsOverlapping( r, true ) );

                // If it is then lets destroy this room and move onto another
                if (overlap)
                {
                    print( "Failure" );
                    DestroyImmediate( room.gameObject );


                    if (stepThrough)
                    {
                        await PauseForRoomPlacement();
                    }

                    continue;
                }

                // Otherwise, save the room to the list.
                rooms.Add( room );
                room.Create();
                successes++;

                // Assign the room neighbours
                AssignRoomNeighbours( room, adjoiningRoom, direction );


                if (stepThrough )
                {
                    await PauseForRoomPlacement();
                }


            }

        }

        private Task PauseForRoomPlacement()
        {
            return Task.Delay( TimeSpan.FromSeconds( 0.2f ) );
        }

        /// <summary>
        ///  TODO: Change args to use RoomTransitionRelationship
        /// </summary>
        /// <param name="room"></param>
        /// <param name="adjoiningRoom"></param>
        /// <param name="direction"></param>
        private static void AssignRoomNeighbours( Room room, Room adjoiningRoom, Vector3 direction )
        {
            adjoiningRoom.neighbours[direction] = room;
            var newDir = Vector3.Reflect( direction, direction );
            room.neighbours[newDir] = adjoiningRoom;
        }

        private Vector3 DecideRoomPlacement( Room room, Room adjoiningRoom, Vector3 direction )
        {
            Vector3 target;

            // Need to offset when going left or down, e.g. offset by prevRoom - room
            int widthOffset = Mathf.Approximately( direction.x, -1 ) ? (adjoiningRoom.width - room.width) - 1 : 1;
            int heightOffset = Mathf.Approximately( direction.z, -1 ) ? (adjoiningRoom.height - room.height) - 1 : 1;
            var edgeOffset = new Vector3( adjoiningRoom.width * direction.x + widthOffset, 0, adjoiningRoom.height * direction.z + heightOffset );

            // Make some random offset along the chosen edge, makes it look less square.
            int vx = Random.Range( 1, adjoiningRoom.width / 2 );
            int vz = Random.Range( 1, adjoiningRoom.height / 2 );
            // Flipping the direction z and x multiplication here neatly allows me to switch from moving towards an edge to moving along it
            // It's just easy to miss so watch out for that.
            var variationOffset = new Vector3( vx * direction.z, 0, vz * direction.x );

            target = adjoiningRoom.transform.position + edgeOffset + variationOffset;
            // Anything else that needs to happen to target?
            return target;
        }

        private Vector3 ChooseRoomDirection( Room room )
        {
            // Could be made recursive

            // TODO: Maybe check first to see if this direction is already used for that room?
            // Currently doesn't work, but it doesn't break so I'll leave this here as WIP

            Vector3 direction;
            int it = 1;
            do
            {
                // Pick a direction, north south east or west
                direction = directions.random();
                it++;
            } while (room.neighbours.Keys.Contains( direction ));

            print( string.Format( "It took {0} attempts to find a good direction", it ) );

            return direction;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append( "There are " + rooms.Count() + " rooms" );
            for (int x = 0; x < dungeonSize.x; x++)
            {
                for (int y = 0; y < dungeonSize.y; y++)
                {

                }
            }
            return sb.ToString();
        }

        public void EmptyContents( bool purgeList )
        {
            for (int i = roomParent.childCount; i > 0; i--) // Safer to go back to front?
            {
                var child = roomParent.GetChild( 0 );
                DestroyImmediate( child.gameObject );
            }

            if (purgeList)
            {
                rooms = new List<Room>();
            }
        }
    }
}