using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities
{
    internal class MongoSecurityRights : MongoBaseEntity
    {
        public static readonly string CollectionName = "ProjectSecurityRights";

        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        [BsonElement("projectId")]
        public ObjectId ProjectId { get; set; }

        [BsonElement("canViewProject")]
        public bool CanViewProject { get; set; }

        [BsonElement("canChangeRequirements")]
        public bool CanChangeRequirements { get; set; }

        [BsonElement("canChangeUseCases")]
        public bool CanChangeUseCases { get; set; }

        [BsonElement("canChangeProjectRights")]
        public bool CanChangeProjectRights { get; set; }

        [BsonElement("isAdministrator")]
        public bool IsAdministrator { get; set; }
    }
}
