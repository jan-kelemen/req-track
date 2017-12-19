using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions
{
    public static class IdentityExtensions
    {
        public static ObjectId ToMongoIdentity(this Identity id) => ObjectId.Parse(id.ToString());

        public static Identity ToDomainIdentity(this ObjectId id) => Identity.FromString(id.ToString());
    }
}
