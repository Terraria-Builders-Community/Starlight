using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public abstract class PlayerResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnGreetPlayerAsync(OnGreetPlayerArgs args)
            => Continue();

        public virtual Task<HandleResult> OnLeavePlayerAsync(OnLeavePlayerArgs args)
            => Continue();

        public virtual Task<HandleResult> OnNameCollisionAsync(OnNameCollisionArgs args)
            => Continue();
    }
}
