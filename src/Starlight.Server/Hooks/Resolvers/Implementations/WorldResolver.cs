namespace Starlight
{
    public abstract class WorldResolver : AbstractedResolver
    {
        public virtual Task<HandleResult> OnGameWorldConnectAsync()
            => Continue();

        public virtual Task<HandleResult> OnGameWorldDisconnectAsync()
            => Continue();

        public virtual Task<HandleResult> OnHardmodeTileUpdateAsync(OnHardmodeTileUpdateArgs args)
            => Continue();

        public virtual Task<HandleResult> OnHardmodeTilePlaceAsync(OnHardmodeTilePlaceArgs args)
            => Continue();
    }
}
