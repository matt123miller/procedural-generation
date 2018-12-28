using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {

    public class Door : ProceduralObject
    {

        public virtual void SetupRoom(int _width, int _height, Material _material)
        { 
            CreateDoor(_width, _height);
        }
    }
}
