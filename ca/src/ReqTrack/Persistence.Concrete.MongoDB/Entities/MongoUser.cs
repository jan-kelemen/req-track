using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    internal class MongoUser : MongoBaseEntity
    {
        public static readonly string CollectionName = "Users";

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("displayName")]
        public string DisplayName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
