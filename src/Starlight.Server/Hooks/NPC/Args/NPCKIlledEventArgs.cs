using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starlight
{
    public sealed class NPCKilledEventArgs
    {
        public NPC Npc;

        public NPCKilledEventArgs(NPC npc)
        {
            Npc = npc;
        }
    }
}
