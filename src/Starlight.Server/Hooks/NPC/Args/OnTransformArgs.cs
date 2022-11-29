using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Starlight
{
    public sealed class OnTransformArgs { 
            
          public int NpcId { get; set; }  


        public OnTransformArgs(int npcId)
        {
            NpcId = npcId;
        }
    }
}
