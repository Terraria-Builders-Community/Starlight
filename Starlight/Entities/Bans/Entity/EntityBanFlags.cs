namespace Starlight.Entities.Bans.Entity
{
    [Flags]
    public enum EntityBanFlags : byte
    {
        /// <summary>
        ///     00000
        /// </summary>
        None = 0,

        /// <summary>
        ///     00001
        /// </summary>
        Name = 1,

        /// <summary>
        ///     00011
        /// </summary>
        Account = 2,

        /// <summary>
        ///     00111
        /// </summary>
        UUID = 4,

        /// <summary>
        ///     01111
        /// </summary>
        IP = 8,
    }
}
