using Terraria;

namespace Starlight
{
    public sealed class OnNPCAIUpdateArgs
    {
        public NPC Self { get; set; }

        public OnNPCAIUpdateArgs(NPC self)
        {
            Self = self;
        }
    }
}