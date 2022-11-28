using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public sealed class OnPreCommandArgs
    {
        public ChatCommandContext Context { get; }

        public OnPreCommandArgs(ChatCommandContext context)
        {
            Context = context;
        }
    }
}
