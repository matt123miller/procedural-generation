using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Can be useful but I want to make a class/struct of this instead so it's usable everywhere.
using Neighbours = System.Collections.Generic.Dictionary<UnityEngine.Vector3, Dungeon.Room>;

namespace Dungeon
{

    public class Door : ProceduralObject
    {
        //public Neighbours neighbours = new Neighbours();
        public Dictionary<Vector3, Room> neighbours = new Dictionary<Vector3, Room>();

        // Could possibly add initialiser with options for materials, animations, whatever.

        public void InitialiseWithData( int x, int z )
        {
            InitialiseWithData( new Vector3( x, 0, z ) );
        }

        public void InitialiseWithData( Vector3 position )
        {
            SetPosition( position );
        }

        public void CacheNeighbours( RoomTransitionRelationship relationship )
        {
            // Probably correct. Revisit if necessary
            neighbours.Add( relationship.via, relationship.to );
            var reflectedDirection = Vector3.Reflect( relationship.via, relationship.via );
            neighbours.Add( reflectedDirection, relationship.from );
        }
    }
}
