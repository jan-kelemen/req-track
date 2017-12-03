using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    class Project
    {
        public static readonly string CollectionName = "Projects";

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
