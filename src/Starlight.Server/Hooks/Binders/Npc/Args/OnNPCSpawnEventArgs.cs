using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Items;
using Terraria;

namespace Starlight
{
    public sealed class OnNPCSpawnArgs
    {
        public int NPCId { get; set; }

        public int Type { get; set; }

        public OnNPCSpawnArgs(int index, int type)
        {
            NPCId = index;
            int Type = type;
        }

        //may be a nice-to-have, idfk up to u rozen to keep or nah
        public string RetrieveTypeName()
        {
            return NPC.GetFirstNPCNameOrNull(Type);
        }

    }
}