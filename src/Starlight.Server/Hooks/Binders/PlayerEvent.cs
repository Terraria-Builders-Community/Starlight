using Terraria;

namespace Starlight.Hooks.Binders
{
    public abstract class PlayerEvent
    {
        public abstract int PlayerIndex { get; }

        public virtual Player GetPlayer()
        {
            return Main.player[PlayerIndex];
        }
    }
}
