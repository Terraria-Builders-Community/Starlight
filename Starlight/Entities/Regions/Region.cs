using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Starlight.Structures;

namespace Starlight.Entities.Regions
{
    /// <inheritdoc/>
    public record class Region : IRegion
    {
        /// <inheritdoc/>
        [BsonId]
        public ObjectId ObjectId { get; set; }

        /// <inheritdoc/>
        public ModelState State { get; set; } = ModelState.Deserializing;

        private RegionFlags _flags;
        /// <inheritdoc/>
        public RegionFlags Flags
        {
            get
                => _flags;
            set
            {
                _ = this.ModifyAsync(x => x.Flags, value);
                _flags = value;
            }
        }

        private string _name = string.Empty;
        /// <inheritdoc/>
        public string Name
        {
            get
                => _name;
            set
            {
                _ = this.ModifyAsync(x => x.Name, value);
                _name = value;
            }
        }

        private Guid _worldId;
        /// <inheritdoc/>
        public Guid WorldId
        {
            get
                => _worldId;
            set
            {
                _ = this.ModifyAsync(x => x.WorldId, value);
                _worldId = value;
            }
        }

        private Polygon _definition;
        /// <inheritdoc/>
        public Polygon Definition
        {
            get
                => _definition;
            set
            {
                _ = this.ModifyAsync(x => x.Definition, value);
                _definition = value;
            }
        }

        private byte _zlayer;
        /// <inheritdoc/>
        public byte ZLayer
        {
            get
                => _zlayer;
            set
            {
                _ = this.ModifyAsync(x => x.ZLayer, value);
                _zlayer = value;
            }
        }
    }
}
