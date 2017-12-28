using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    public class MongoRequirement
    {
        public static readonly string CollectionName = "Requirements";

        [BsonId]
        public ObjectId Id { get; set; }

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
    }
}
