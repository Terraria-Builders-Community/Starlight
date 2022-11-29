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
            Type = type;
        }
    }
}