namespace Starlight
{
    public abstract class ItemResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnQuickStackAsync(OnQuickStackArgs args)
            => Continue();

        public virtual Task<HandleResult> OnSetDefaultsAsync(OnSetItemDefaultArgs args)
            => Continue();

        public virtual Task<HandleResult> OnNetDefaultsAsync(OnSetItemDefaultArgs args)
            => Continue();
    }
}
