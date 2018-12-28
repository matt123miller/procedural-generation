using UnityEngine;

namespace Dungeon
{
    public class Corridor : Room
    {
        public override void InitialiseWithData(int id, int _width, int _height, Material _material)
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
            base.InitialiseWithData(id, w, h, _material);
        }

    }
}
