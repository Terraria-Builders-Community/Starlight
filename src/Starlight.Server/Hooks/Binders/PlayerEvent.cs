using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
