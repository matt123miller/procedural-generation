using UnityEngine;
using System.Collections;
using Dungeon;

public class Corridor : Room
{
    public override void InitialiseRoomData(int _width, int _height, Material _material)
    {
        bool horizontal = Random.Range(0, 2) % 2 == 0;
        int w, h;

        if (horizontal)
        {
            w = _width * 2;
            h = 4;
        }
        else
        {
            w = 4;
            h = _height * 2;
        }
        print("LOL THIS IS A CORRIDOR NOT A ROOM, WHAT A TROLL!");
        base.InitialiseRoomData(w, h, _material);
    }

}
