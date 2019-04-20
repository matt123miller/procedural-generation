using UnityEngine;

namespace Dungeon
{
    public struct RoomTransitionRelationship
    {
        internal readonly Room from;
        internal readonly Room to;
        internal readonly Vector3 via;

        public RoomTransitionRelationship( Room from, Room to, Vector3 via )
        {
            this.from = from;
            this.to = to;
            this.via = via;
        }

        public RoomTransitionRelationship Flip()
        {
            return new RoomTransitionRelationship(this.to, this.from, OppositeDirection());
        }

        public Vector3 OppositeDirection()
        {
            return Vector3.Reflect(via, via);
        }

        public override string ToString()
        {
            var str = $"Travelling from room {from.roomID} to room {to.roomID} in the direction of {via}";

            return str;
        }
    }
}