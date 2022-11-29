using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Starlight
{
    public class OnNPCLootDropArgs
    {
        public IEntitySource Source { get; set; }

        public Vector2 Position { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Stack { get; set; }

        public int ItemId { get; set; }

        public bool Broadcast { get; set; }

        public int Prefix { get; set; }

        public int NpcId { get; set; }

        public int NpcArrayIndex { get; set; }

        public bool NoGrabDelay { get; set; }

        public bool ReverseLookup { get; set; }

        public OnNPCLootDropArgs(IEntitySource source, Vector2 position, int width, int height, int stack, int itemId, bool broadcast, int prefix, int npcId, int npcArrayIndex, bool noGrabDelay, bool reverseLookup)
        {
            Source = source;
            Position = position;
            Width = width;
            Height = height;
            Stack = stack;
            ItemId = itemId;
            Broadcast = broadcast;
            Prefix = prefix;
            NpcId = npcId;
            NpcArrayIndex = npcArrayIndex;
            NoGrabDelay = noGrabDelay;
            ReverseLookup = reverseLookup;
        }
    }
}
