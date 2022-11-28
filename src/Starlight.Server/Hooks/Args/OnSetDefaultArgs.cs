using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Items;
using Terraria;

namespace Starlight
{
    public sealed class OnSetDefaultArgs
    {
        public Item Item { get; internal set; }

        public int Type { get; set; }

        public ItemVariant? ItemVariant { get; set; }

        public OnSetDefaultArgs(Item item, ref int type, ItemVariant? itemVariant)
        {
            Item = item;
            Type = type;
            ItemVariant = itemVariant;
        }
    }
}
