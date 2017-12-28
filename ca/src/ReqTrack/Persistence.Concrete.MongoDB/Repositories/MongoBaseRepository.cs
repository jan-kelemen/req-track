using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;

namespace Persistence.Concrete.MongoDB.Repositories
{
    public abstract class MongoBaseRepository : IRepository
    {
        public Identity GenerateNewIdentity() => Identity.FromString(ObjectId.GenerateNewId().ToString());
    }
}
