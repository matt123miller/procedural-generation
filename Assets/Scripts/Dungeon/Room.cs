using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Dungeon
{

    public class Room : ProceduralObject
    {
        public int roomID;
        private Material material;
        public Transform[,] tiles;
        public Transform[] walls;

        public Dictionary<Vector3, Room> neighbours;

        public virtual void InitialiseWithData(int id, int _width, int _height, Material _material)
        {
            roomID = id;
            width = _width;
            height = _height;
            _centre = new Vector3(width / 2, 0, height / 2);
            material = _material;
            neighbours = new Dictionary<Vector3, Room>();
        }

        public void Create()
        {
            tiles = GenerateTiles();
            walls = GenerateWalls();
        }

        private Transform[,] GenerateTiles()
        {
            Transform[,] tiles = new Transform[width, height];
            
            // Resharper told me to cache this to avoid going between manager and unmanaged code twice.
            // Good reason I guess.
            var pos = transform.position;
            var offsetX = (int)pos.x;
            var offsetZ = (int)pos.z;
            
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    // Offset by 1 all the time so they're inside the walls.
                    var tile = CreateFloor(w + offsetX +  1, h + offsetZ + 1);
                    tile.GetComponent<MeshRenderer>().sharedMaterial = material;
                    tiles[w, h] = tile.transform;
                    transform.AddChild(tile.transform);
                }
            }

            return tiles;
        }

        private Transform[] GenerateWalls()
        {
            List<Transform> walls = new List<Transform>();

            var pos = transform.position;
            var offsetX = (int)pos.x;
            var offsetZ = (int)pos.z;
            
            int[] edge = { 0, 1 };
            foreach (var i in edge)
            {
                int z = (height + 1) * i + offsetZ;
                // Make the walls along the top and bottom edges
                for (int w = 1; w <= width; w++)
                {
                    var wall = CreateWall(w + offsetX, z );
                    walls.Add(wall.transform);
                    wall.name = i == 0 ? "bottom" : "top";
                }

                int x = (width + 1) * i + offsetX;
                // Make the walls along left and right edges
                for (int h = 1; h <= height; h++)
                {
                    var wall = CreateWall(x, h + offsetZ);
                    walls.Add(wall.transform);
                    wall.name = i == 0 ? "left" : "right";
                }
            }

            // Easier to use simpler loops and create 4 corners manually
            walls.Add(CreateWall(offsetX, offsetZ).transform);
            walls.Add(CreateWall(offsetX, offsetZ + height + 1).transform);
            walls.Add(CreateWall(offsetX + width + 1, offsetZ).transform);
            walls.Add(CreateWall(offsetX + width + 1, offsetZ + height + 1).transform);

            return walls.ToArray();
        }


        public void PlaceRandomly(Vector2 inBounds)
        {
            int randomX = (int)Random.Range(0, inBounds.x - width);
            int randomZ = (int)Random.Range(0, inBounds.y - height);

            SetPosition(new Vector3(randomX, 0, randomZ));
        }


        public bool IsOverlapping(Room _other, bool _floorDimensions)
        {
            if (!_other) { return false; }
            bool topLeft = false;
            bool bottomRight = false;

            if (_floorDimensions)
            {
                // Unsure about all the -2.
                // Doesn't that just all cancel out?
                topLeft = this.Left - 2 < _other.Right - 2 && this.Right - 2 > _other.Left - 2;
                bottomRight = this.Top - 2 > _other.Bottom - 2 && this.Bottom - 2 < _other.Top - 2;
            }
            else
            {
                topLeft = this.Left < _other.Right && this.Right > _other.Left;
                bottomRight = this.Top > _other.Bottom && this.Bottom < _other.Top;
            }
            return topLeft && bottomRight;
        }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();

        //    for (int y = 0; y <= height; y++)
        //    {
        //        for (int x = 0; x <= width; x++)
        //        {
        //            if (y == 0 || y == height || x == 0 || x == width)
        //            {
        //                sb.Append("W");
        //            }
        //            else
        //            {
        //                sb.Append("F");
        //            }

        //            if (x == width)
        //                sb.Append("\n");
        //        }
        //    }
        //    return sb.ToString();
        //}
    }
}
