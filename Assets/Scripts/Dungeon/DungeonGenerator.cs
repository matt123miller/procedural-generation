using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Dungeon
{
    public class DungeonGenerator : ProceduralGenerator
    {
        [Range(40, 60)] public int minWidth;
        [Range(60, 120)] public int maxWidth;
        [Range(40, 60)] public int minHeight;
        [Range(60, 120)] public int maxHeight;

        public Vector2 dungeonSize;

        public RoomGenerator roomGen;
        private void Awake()
        {
            CreateAllDungeon();
        }

        public void CreateAllDungeon()
        {
            Generate();

            roomGen = GetComponent<RoomGenerator>();
            roomGen.Generate(dungeonSize);
        }

        public override void Generate()
        {
            int w = Random.Range(minWidth, maxWidth);
            int h = Random.Range(minHeight, maxHeight);
            dungeonSize = new Vector2(w, h);
        }

    }
}
