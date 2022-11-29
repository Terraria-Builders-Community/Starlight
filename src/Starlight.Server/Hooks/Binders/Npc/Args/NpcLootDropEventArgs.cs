﻿using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Starlight
{
    public class OnNPCLootDropArgs
    {
        public IEntitySource Source
        {
            get;
            set;
        }
        public Vector2 Position
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }
        public int Stack
        {
            get;
            set;
        }
        public int ItemId
        {
            get;
            set;
        }
        public bool Broadcast
        {
            get;
            set;
        }
        public int Prefix
        {
            get;
            set;
        }
        public int NpcId
        {
            get;
            internal set;
        }
        public int NpcArrayIndex
        {
            get;
            internal set;
        }

        public bool NoGrabDelay
        {
            get; set;
        }

        public bool ReverseLookup
        {
            get; set;
        }

    }
}
