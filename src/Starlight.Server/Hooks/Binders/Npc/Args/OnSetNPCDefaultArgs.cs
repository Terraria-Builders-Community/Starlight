using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Items;
using Terraria;

namespace Starlight
{
    public sealed class OnSetNPCDefaultArgs
    {
        public NPC NPC { get; internal set; }

        public int Type { get; set; }

        public OnSetNPCDefaultArgs(NPC npc, int type)
        {
            NPC = npc;
            Type = type;
        }
    }
}
