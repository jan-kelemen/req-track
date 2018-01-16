using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    internal class MongoBaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
