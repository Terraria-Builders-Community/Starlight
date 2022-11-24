using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Starlight.Entities.Bans.Entity
{
    public record class UserBan : IEntityBan
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        public ModelState State { get; set; }

        private EntityBanFlags _flags;
        /// <summary>
        ///     The flags that determine the ban type.
        /// </summary>
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

        public string Name { get; set; }

        public DateTimeOffset? TimeUntilRaised { get; set; }

        public ulong Id { get; set; }
    }
}
