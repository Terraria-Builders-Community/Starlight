using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public abstract class AbstractedResolver : IResolver
    {
        public int Order { get; set; }

        protected virtual Task<HandleResult> Continue()
            => Task.FromResult(HandleResult.Continue());

        protected virtual Task<HandleResult> Break()
            => Task.FromResult(HandleResult.Break());
    }
}
