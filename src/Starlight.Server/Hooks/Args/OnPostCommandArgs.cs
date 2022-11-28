using CSF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public sealed class OnPostCommandArgs
    {
        public IResult Result { get; }

        public ChatCommandContext Context { get; }

        public OnPostCommandArgs(IResult result, ChatCommandContext context)
        {
            Result = result;
            Context = context;
        }
    }
}
