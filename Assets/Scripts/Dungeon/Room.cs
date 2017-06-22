using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{

    public class Room : MonoBehaviour
    {
#region Rect bounds
        public float Top { 
            get {
                return transform.position.z + height;
            }
        }
        public float Bottom { 
            get {
                return transform.position.z;
            }
        }
        public float Left { 
            get {
                return transform.position.x;
            }
        }
        public float Right { 
            get {
                return transform.position.x + width;
            }
        }
#endregion
        // Always treat width as X and height as Y;
        public int width, height;
        private int floorWidth, floorHeight;
        public Vector3 centre;
        private Material material;
        public Transform[,] tiles;
        public Transform[] walls;


        public void SetupRoom(int _width, int _height, Material _material)
        {
            width = _width;
            height = _height;
            floorWidth = _width - 2;
            floorHeight = _height - 2;
            centre = new Vector3(width / 2, 0, height / 2);
            material = _material;

            tiles = GenerateTiles();
            walls = GenerateWalls();
        }

        public Transform[,] GenerateTiles()
        {
            Transform[,] tiles = new Transform[width, height];

            for (int w = 0; w < floorWidth; w++)
            {
                for (int h = 0; h < floorHeight; h++)
                {
                    // Offset by 1 all the time so they're inside the walls.
                    var tile = CreateFloor(w + 1, h + 1);
                    tiles[w, h] = tile.transform;
                }
            }

            return tiles;
        }

        public Transform[] GenerateWalls()
        {
            List<Transform> walls = new List<Transform>();

            int[] edge = { 0, 1 };
            foreach (var i in edge)
            {
                int z = (height - 1) * i;
                // Make the walls along the width
                for (int w = 0; w < floorWidth; w++)
                {
                    var wall = CreateWall(w + 1, z);
                    walls.Add(wall.transform);
                    wall.name = "width";
                }
                int x = (width - 1) * i;
                // Make the walls along height
                for (int h = 0; h < floorHeight; h++)
                {
                    var wall = CreateWall(x, h + 1);
                    walls.Add(wall.transform);
                    wall.name = "height";
                }
            }

            // Easier to use simpler loops and create 4 corners manually
            walls.Add(CreateWall(0, 0).transform);
            walls.Add(CreateWall(0, height -1).transform);
            walls.Add(CreateWall(width -1, 0).transform);
            walls.Add(CreateWall(width -1, height -1).transform);

            return walls.ToArray();
        }

        private GameObject CreateWall(int x, int z)
        {
            var wall = CreatePrimitive(x, z, PrimitiveType.Cube);
            wall.transform.Translate(0, 0.5f, 0);
            wall.layer = Layers.Wall;
            wall.name = "Wall";
            return wall;
        }

        private GameObject CreateFloor(int x, int z)
        {
            var tile = CreatePrimitive(x, z, PrimitiveType.Quad);
            tile.transform.Rotate(90, 0, 0);
            tile.layer = Layers.Floor;
            tile.name = "Floor";
            tile.GetComponent<MeshRenderer>().sharedMaterial = material;
            return tile;
        }

        private GameObject CreatePrimitive(int x, int z, PrimitiveType type)
        {
            var primitive = GameObject.CreatePrimitive(type);
            primitive.transform.position = new Vector3(x, 0, z);
            primitive.transform.SetParent(transform);
            return primitive;
        }


        public void PlaceRandomly(Vector2 inBounds)
        {
            int randomX = (int)Random.Range(0, inBounds.x - width);
            int randomY = (int)Random.Range(0, inBounds.y - height);

            transform.position = new Vector3(randomX, 0, randomY);
        }

        public bool IsOverlapping(Room _other){
            
            if(!_other) { return false; }
            var topLeft = this.Left < _other.Right && this.Right > _other.Left;
            var bottomRight = this.Top > _other.Bottom && this.Bottom < _other.Top;
            return topLeft && bottomRight;
        }
    }
}
