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

        public virtual Task<HandleResult> OnReloadAsync()
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnGetDataAsync()
        {
            return Continue();
        }

        protected virtual Task<HandleResult> Continue()
            => Task.FromResult(HandleResult.Continue());

        protected virtual Task<HandleResult> Break()
            => Task.FromResult(HandleResult.Break());
    }
}
