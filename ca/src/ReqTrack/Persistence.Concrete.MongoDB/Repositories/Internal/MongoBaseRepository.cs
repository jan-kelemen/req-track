using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal
{
    internal abstract class MongoBaseRepository : IRepository
    {
        public Identity GenerateNewIdentity() => Identity.FromString(ObjectId.GenerateNewId().ToString());

        protected T FindByIdOrNull<T>(IMongoCollection<T> collection, ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return collection.FindSync(filter).FirstOrDefault();
        }

        protected T FindByIdOrThrow<T>(IMongoCollection<T> collection, ObjectId id, string message = "")
        {
            var rv = FindByIdOrNull(collection, id);
            if (rv == null)
            {
                throw new AbandonedMutexException(message);
            }

            return rv;
        }
    }
}
