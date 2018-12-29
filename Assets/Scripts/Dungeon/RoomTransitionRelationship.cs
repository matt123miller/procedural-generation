using UnityEngine;

namespace Dungeon
{
    public struct RoomTransitionRelationship
    {
        internal readonly Room from;
        internal readonly Room to;
        internal readonly Vector3 via;

        public RoomTransitionRelationship(Room from, Room to, Vector3 via)
        {
            this.from = from;
            this.to = to;
            this.via = via;
        }
    }
}