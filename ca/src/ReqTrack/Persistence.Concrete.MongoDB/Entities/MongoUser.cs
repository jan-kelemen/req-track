using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    public class MongoUser
    {
        public static readonly string CollectionName = "Users";

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("displayName")]
        public string DisplayName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
