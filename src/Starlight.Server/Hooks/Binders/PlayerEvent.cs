using Terraria;

namespace Starlight
{
    public abstract class PlayerEvent
    {
        public abstract int PlayerIndex { get; set; }

        public virtual Player GetPlayer()
        {
            return Main.player[PlayerIndex];
        }
    }
}
