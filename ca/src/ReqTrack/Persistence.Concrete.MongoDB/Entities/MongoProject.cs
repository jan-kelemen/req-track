using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    internal class MongoProject : MongoBaseEntity
    {
        public static readonly string CollectionName = "Projects";

        [BsonElement("authorId")]
        public ObjectId AuthorId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
