using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {

    public class Door : ProceduralObject
    {
        public Dictionary<Vector3, Room> neighbours = new Dictionary<Vector3, Room>();
    
        public Dictionary<Vector3, Room> neighbours;

        // Could possibly add initialiser with options for materials, animations, whatever.
        
        public void InitialiseWithData(int x, int z)
        { 
            InitialiseWithData(new Vector3(x, 0, z));
        }
        
        public void InitialiseWithData(Vector3 position)
        { 
            SetPosition(position);
        }

        public void CacheNeighbours(RoomTransitionRelationship relationship)
        {
            // Probably correct. Revisit if necessary
            neighbours.Add(relationship.via, relationship.to);
            var reflectedDirection = Vector3.Reflect(relationship.via, relationship.via);
            neighbours.Add(reflectedDirection, relationship.from);
        }
    }
}
