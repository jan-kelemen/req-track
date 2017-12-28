using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    public class MongoUseCase
    {
        public static readonly string CollectionName = "UseCases";

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("projectId")]
        public ObjectId ProjectId { get; set; }

        [BsonElement("authorId")]
        public ObjectId AuthorId { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }

        [BsonElement("steps")]
        public IEnumerable<string> Steps { get; set; }

        [BsonElement("orderMarker")]
        public int OrderMarker { get; set; }
    }
}
