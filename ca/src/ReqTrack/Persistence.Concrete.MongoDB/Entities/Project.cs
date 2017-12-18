using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    internal class Project
    {
        internal class Requirement
        {
            [BsonElement("id")]
            public ObjectId Id { get; set; }

            [BsonElement("type")]
            public string Type { get; set; }

            [BsonElement("orderMarker")]
            public int OrderMarker { get; set; }
        }

        public static readonly string CollectionName = "Projects";

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("requirements")]
        public IEnumerable<Requirement> Requirements { get; set; }
    }
}
