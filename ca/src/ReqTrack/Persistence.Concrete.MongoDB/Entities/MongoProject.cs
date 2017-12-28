using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    public class MongoProject
    {
        public static readonly string CollectionName = "Projects";

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("authorId")]
        public ObjectId AuthorId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
