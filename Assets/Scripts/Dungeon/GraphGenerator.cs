using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class GraphGenerator : MonoBehaviour
    {
        public List<Room> rooms;

        public Transform Generate( List<Room> _rooms )
        {
            rooms = _rooms;



            // DEFINITELY CHANGE LATER, RETURN THE GRAPH
            return transform;
        }
    }
}
