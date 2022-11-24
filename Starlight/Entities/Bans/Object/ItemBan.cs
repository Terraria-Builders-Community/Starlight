using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Starlight.Entities.Bans.Object
{
    public record class ItemBan : IObjectBan
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        public ModelState State { get; set; }

        public uint Id { get; set; }

        public DateTimeOffset? TimeUntilRaised { get; set; }
    }
}
