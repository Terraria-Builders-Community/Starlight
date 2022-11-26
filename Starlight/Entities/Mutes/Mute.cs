using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Starlight.Entities.Bans.Entity;
using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Mutes
{
    /// <inheritdoc/>
    public record class Mute : IMute
    {
        /// <inheritdoc/>
        [BsonId]
        public ObjectId ObjectId { get; set; }

        /// <inheritdoc/>
        public ModelState State { get; set; } = ModelState.Deserializing;

        private EntityBanFlags _flags;
        /// <inheritdoc/>
        public EntityBanFlags Flags
        {
            get
                => _flags;
            set
            {
                _ = this.ModifyAsync(x => x.Flags, value);
                _flags = value;
            }
        }

        private UUId _uuid;
        /// <summary>
        ///     The UUID of the user that was banned.
        /// </summary>
        public UUId UUID
        {
            get
                => _uuid;
            set
            {
                _ = this.ModifyAsync(x => x.UUID, value);
                _uuid = value;
            }
        }

        private DateTimeOffset? _timeUntilRaised;
        /// <inheritdoc/>
        public DateTimeOffset? TimeUntilRaised
        {
            get
                => _timeUntilRaised;
            set
            {
                _ = this.ModifyAsync(x => x.TimeUntilRaised, value);
                _timeUntilRaised = value;
            }
        }
    }
}
