using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public abstract class NetResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnGetDataAsync(OnGetDataArgs args)
            => Continue();

        public virtual Task<HandleResult> OnReceiveDataAsync(OnReceiveDataArgs args)
            => Continue();

        public virtual Task<HandleResult> OnSendBytesAsync(OnSendBytesArgs args)
            => Continue();

        public virtual Task<HandleResult> OnSendNetDataAsync(OnSendNetDataArgs args)
            => Continue();

        public virtual Task<HandleResult> OnSendDataAsync(OnSendDataArgs args)
            => Continue();

        public virtual Task<HandleResult> OnSocketResetAsync(OnSocketResetArgs args)
            => Continue();
    }
}
