using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public abstract class ProjectileResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnSetDefaultsAsync(OnSetProjectileDefaultArgs args)
            => Continue();

        public virtual Task<HandleResult> OnProjectileAIUpdateAsync(OnProjectileAIUpdateArgs args)
            => Continue();
    }
}
