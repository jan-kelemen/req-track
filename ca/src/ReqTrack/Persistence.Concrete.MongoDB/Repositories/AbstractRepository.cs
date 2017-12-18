using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    internal abstract class AbstractRepository : IRepository
    {
        public Identity GenerateNewIdentity()
        {
            return ObjectId.GenerateNewId().ToDomainIdentity();
        }
    }
}
