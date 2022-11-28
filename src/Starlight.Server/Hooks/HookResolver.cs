using CSF;

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

        public virtual Task<HandleResult> OnQuickStackAsync(OnQuickStackArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnSetDefaultsAsync(OnSetDefaultArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnNetDefaultsAsync(OnSetDefaultArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnReceiveDataAsync(OnReceiveDataArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnSendBytesAsync(OnSendBytesArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnSendNetDataAsync(OnSendNetDataArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnSendDataAsync(OnSendDataArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnGreetPlayerAsync(OnGreetPlayerArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnNameCollisionAsync(OnNameCollisionArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnChatBroadcastAsync(OnBroadcastArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnHardmodeTileUpdateAsync(OnHardmodeTileUpdateArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnHardmodeTilePlaceAsync(OnHardmodeTilePlaceArgs args)
        {
            return Continue();
        }

        public virtual Task<HandleResult> InvokeGameUpdateAsync()
        {
            return Continue();
        }

        public virtual Task<HandleResult> InvokePostGameUpdateAsync()
        {
            return Continue();
        }
        public virtual Task<HandleResult> OnStatueSpawnAsync()
        {
            return Continue();
        }

        public virtual Task<HandleResult> OnPostInitializeAsync()
        {
            return Continue();
        }

        protected virtual Task<HandleResult> Continue()
            => Task.FromResult(HandleResult.Continue());

        protected virtual Task<HandleResult> Break()
            => Task.FromResult(HandleResult.Break());

    }
}
