namespace Starlight
{
    public abstract class GameResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnReloadAsync(OnReloadArgs args)
            => Continue();

        public virtual Task<HandleResult> OnGameUpdateAsync()
            => Continue();

        public virtual Task<HandleResult> OnPostGameUpdateAsync()
            => Continue();

        public virtual Task<HandleResult> OnPostInitializeAsync()
            => Continue();
    }
}
