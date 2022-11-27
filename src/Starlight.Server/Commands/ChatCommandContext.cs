using CSF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public class ChatCommandContext : CommandContext
    {
        public ChatCommandContext(string rawInput, IPrefix? prefix = null) 
            : base(rawInput, prefix)
        {

        }
    }
}
