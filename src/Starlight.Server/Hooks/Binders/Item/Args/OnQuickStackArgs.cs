using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starlight
{
    public sealed class OnQuickStackArgs
    {
        public Player Player { get; set; }

        public Chest Chest { get; set; }

        public Item Item { get; set; }

        public Vector2 WorldPosition
            => new(Chest.x * 16 + 16, Chest.y * 16 + 16);

        public Vector2 TilePosition
            => new(Chest.x, Chest.y);

        public OnQuickStackArgs(Player player, Chest chest, Item item)
        {
            Player = player;
            Chest = chest;
            Item = item;
        }
    }
}
