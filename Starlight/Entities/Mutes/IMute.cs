using Starlight.Structures;

namespace Starlight.Entities.Mutes
{
    /// <summary>
    ///     Represents a user mute.
    /// </summary>
    public interface IMute : IModel
    {
        /// <summary>
        ///     Represents the user UUID.
        /// </summary>
        public UUId UUID { get; set; }

        /// <summary>
        ///     The timestamp when this mute will be raised. <see langword="null"/> if permanent.
        /// </summary>
        public DateTimeOffset? TimeUntilRaised { get; set; }
    }
}
