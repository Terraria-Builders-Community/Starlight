using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Server.Server
{
    public readonly struct HandleResult : IHandleResult
    {
        public bool Handled { get; }

        private HandleResult(bool handled)
            => Handled = handled;

        public static HandleResult FromHandled()
            => new(true);

        public static HandleResult FromUnhandled()
            => new(false);
    }
}
