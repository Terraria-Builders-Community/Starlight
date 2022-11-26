using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Devices
{
    public record class Device : IDevice
    {
        /// <inheritdoc/>
        [BsonId]
        public ObjectId ObjectId { get; set; }

        /// <inheritdoc/>
        public ModelState State { get; set; } = ModelState.Deserializing;

        private UUId _uuid;
        /// <inheritdoc/>
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

        private IList<IPAddress> _ipHistory = new List<IPAddress>();
        /// <inheritdoc/>
        public IList<IPAddress> IPHistory
        {
            get
                => _ipHistory;
            set
            {
                _ = this.ModifyAsync(x => x.IPHistory, value);
                _ipHistory = value;
            }
        }
    }
}
