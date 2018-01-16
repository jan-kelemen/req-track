using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    internal class MongoRequirement : MongoBaseEntity
    {
        public static readonly string CollectionName = "Requirements";

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("projectId")]
        public ObjectId ProjectId { get; set; }

        [BsonElement("authorId")]
        public ObjectId AuthorId { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }

        [BsonElement("orderMarker")]
        public int OrderMarker { get; set; }
    }
}
