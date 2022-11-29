using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    public abstract class ChatResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnChatBroadcastAsync(OnBroadcastArgs args)
            => Continue();

        public virtual Task<HandleResult> OnChatAsync(OnChatArgs args)
            => Continue();

        public virtual Task<HandleResult> OnPreCommandAsync(OnPreCommandArgs args)
            => Continue();

        public virtual Task<HandleResult> OnServerCommandAsync(OnCommandProcessArgs args)
            => Continue();

        public virtual Task<HandleResult> OnPostCommandAsync(OnPostCommandArgs args)
            => Continue();
    }
}
