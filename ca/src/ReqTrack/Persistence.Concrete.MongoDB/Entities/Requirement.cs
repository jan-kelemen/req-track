using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    internal class Requirement
    {
        public static readonly string CollectionName = "Requirements";

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }
        
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("details")]
        public string Details { get; set; }

        [BsonElement("projectId")]
        public ObjectId ProjectId { get; set; }
    }
}
