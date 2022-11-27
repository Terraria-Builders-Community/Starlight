using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Tests
{
    public class MyHookHandler : HookResolver
    {
        public override Task<HandleResult> OnChatAsync(OnChatArgs args)
        {
            return Continue();
        }

        public override Task<HandleResult> OnGetDataAsync()
        {
            return Break();
        }
    }
}
