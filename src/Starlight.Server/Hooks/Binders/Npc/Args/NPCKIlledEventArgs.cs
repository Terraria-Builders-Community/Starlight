using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starlight
{
    public sealed class OnNPCKilledArgs
    {
        public NPC Npc;

        public OnNPCKilledArgs(NPC npc)
        {
            Npc = npc;
        }
    }
}
