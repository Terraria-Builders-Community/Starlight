using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Items;
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