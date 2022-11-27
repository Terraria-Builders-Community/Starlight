using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Tests
{
    public class MyHookHandler : HookResolver
    {
        public MyHookHandler()
        {
            Order = 1;
        }

        public override Task<HandleResult> OnChatAsync(OnChatArgs args)
        {
            return Continue();
        }

        public override Task<HandleResult> OnGetDataAsync(OnGetDataArgs args)
        {
            return Break();
        }
    }
}
