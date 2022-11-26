using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Bans.Entity
{
    /// <summary>
    ///     Represents a user ban.
    /// </summary>
    public record class UserBan : IEntityBan
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

        private string? _name;
        /// <inheritdoc/>
        public string? Name
        {
            get
                => _name;
            set
            {
                _ = this.ModifyAsync(x => x.Name, value);
                _name = value;
            }
        }

        private UUId? _uuid;
        /// <summary>
        ///     The UUID of the user that was banned.
        /// </summary>
        public UUId? UUID
        {
            get 
                => _uuid;
            set
            {
                _ = this.ModifyAsync(x => x.UUID, value);
                _uuid = value;
            }
        }

        private IPAddress? _ip;
        /// <summary>
        ///     The IP of the user that was banned. 
        /// </summary>
        public IPAddress? IP
        {
            get
                => _ip;
            set
            {
                _ = this.ModifyAsync(x => x.IP, value);
                _ip = value;
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
