namespace Starlight.Entities.Regions
{
    /// <summary>
    ///     Represents the flags that are potentially present on a region
    /// </summary>
    [Flags]
    public enum RegionFlags : ushort
    {
        /// <summary>
        ///     000000000000
        /// </summary>
        None = 0,

        /// <summary>
        ///     000000000001
        /// </summary>
        DisallowNonPermitted = 1,

        /// <summary>
        ///     000000000010
        /// </summary>
        DisallowInteractions = 2,

        /// <summary>
        ///     000000000100
        /// </summary>
        DisallowItemDrops = 4,

        /// <summary>
        ///     000000001000
        /// </summary>
        DisallowBuild = 8,

        /// <summary>
        ///     000000010000
        /// </summary>
        KillEntries = 16,

        /// <summary>
        ///     000000100000
        /// </summary>
        ForcePvP = 32,

        /// <summary>
        ///     000001000000
        /// </summary>
        ForceNoPvP = 64,

        /// <summary>
        ///     000010000000
        /// </summary>
        ForceMobPersistence = 128,

        /// <summary>
        ///     000100000000
        /// </summary>
        ForceNoMobPersistence = 256,

        /// <summary>
        ///     0000000001010
        /// </summary>
        Default = DisallowBuild | DisallowInteractions
    }
}
