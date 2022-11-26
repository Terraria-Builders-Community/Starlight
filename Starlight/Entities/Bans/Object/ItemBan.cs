﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Starlight.Entities.Bans.Object
{
    /// <summary>
    ///     Represents an item ban.
    /// </summary>
    public record class ItemBan : IObjectBan
    {
        /// <inheritdoc/>
        [BsonId]
        public ObjectId ObjectId { get; set; }

        /// <inheritdoc/>
        public ModelState State { get; set; } = ModelState.Deserializing;

        private uint _id;
        /// <inheritdoc/>
        public uint Id
        {
            get
                => _id;
            set
            {
                _ = this.ModifyAsync(x => x.Id, value);
                _id = value;
            }
        }
    }
}
