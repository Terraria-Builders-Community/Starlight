using Terraria;
using Terraria.GameContent.Items;

namespace Starlight
{
    public sealed class OnSetItemDefaultArgs
    {
        public Item Item { get; internal set; }

        public int Type { get; set; }

        public ItemVariant? ItemVariant { get; set; }

        public OnSetItemDefaultArgs(Item item, int type, ItemVariant? itemVariant)
        {
            Item = item;
            Type = type;
            ItemVariant = itemVariant;
        }
    }
}
