using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions
{
    public static class IdentityExtensions
    {
        public static ObjectId ToMongoIdentity(this Identity id)
        {
            return ObjectId.Parse(id.ToString());
        }

        public static Identity ToDomainIdentity(this ObjectId id)
        {
            return Identity.FromString(id.ToString());
        }
    }
}
