namespace Starlight.Entities.Bans.Entity
{
    /// <summary>
    ///     Represents an entity ban.
    /// </summary>
    public interface IEntityBan : IBan
    {
        /// <summary>
        ///     The timestamp when this ban will be raised. <see langword="null"/> if permanent.
        /// </summary>
        public DateTimeOffset? TimeUntilRaised { get; set; }

        /// <summary>
        ///     The flags that determine the ban type.
        /// </summary>
        public EntityBanFlags Flags { get; set; }

        /// <summary>
        ///     The name of the entity that was banned.
        /// </summary>
        public string? Name { get; set; }
    }
}
