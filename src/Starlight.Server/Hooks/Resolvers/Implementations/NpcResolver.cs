namespace Starlight
{
    public abstract class NpcResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnSetDefaultsAsync(OnSetNPCDefaultArgs args)
            => Continue();

        public virtual Task<HandleResult> OnNetDefaultsAsync(OnSetNPCDefaultArgs args)
            => Continue();

        public virtual Task<HandleResult> OnStatueSpawnAsync(OnStatueSpawnArgs args)
            => Continue();

        public virtual Task<HandleResult> OnNPCAIUpdateAsync(OnNPCAIUpdateArgs args)
            => Continue();

        public virtual Task<HandleResult> OnStrikeAsync(OnStrikeArgs args)
            => Continue();

        public virtual Task<HandleResult> OnNPCSpawnAsync(OnNPCSpawnArgs args)
            => Continue();

        public virtual Task<HandleResult> OnNPCKilledAsync(OnNPCKilledArgs args)
            => Continue();

        public virtual Task<HandleResult> OnNPCDropLootAsync(OnNPCLootDropArgs args)
            => Continue();

        public virtual Task<HandleResult> OnBossBagItemAsync(OnBossBagDropArgs args)
            => Continue();

        public virtual Task<HandleResult> OnTransformAsync(OnTransformArgs args)
            => Continue();
    }
}
