using CSF;
using IL.Terraria.GameContent.Bestiary;
using Starlight.Hooks.Args;

namespace Starlight
{
    public abstract class HookResolver : IHookResolver
    {
        public int Order { get; set; }

        public HookResolver()
        {

        }

        public virtual Task<HandleResult> OnChatAsync(OnChatArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnPreCommandAsync(OnPreCommandArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnPostCommandAsync(OnPostCommandArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnReloadAsync(OnReloadArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnGetDataAsync(OnGetDataArgs args)
        {
            return Continue();
        }

        protected virtual Task<HandleResult> Continue()
            => Task.FromResult(HandleResult.Continue());

        protected virtual Task<HandleResult> Break()
            => Task.FromResult(HandleResult.Break());
    }
}
