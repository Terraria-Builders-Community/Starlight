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
