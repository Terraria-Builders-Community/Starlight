using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public sealed class OnGreetPlayerArgs
    {
        public int UserId { get; set; }

        public OnGreetPlayerArgs(int userId)
        {
            UserId = userId;
        }
    }
}
