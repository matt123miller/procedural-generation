using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class GraphGenerator : MonoBehaviour
    {
        public List<Room> rooms;
        
        public void Generate(List<Room> _rooms)
        {
            rooms = _rooms;
        }
    }
}
