using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal
{
    internal class MongoRepository<T> where T : MongoBaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public FilterDefinition<T> IdFilter(ObjectId id) => Builders<T>.Filter.Eq("_id", id);

        public long Count(FilterDefinition<T> filter) => _collection.Count(filter);

        public ObjectId Create(T entity)
        {
            entity.Id = ObjectId.GenerateNewId();
            _collection.InsertOne(entity);
            return entity.Id;
        }

        public T Read(ObjectId id) => Find(IdFilter(id)).FirstOrDefault();

        public IEnumerable<T> Read(IEnumerable<ObjectId> ids)
        {
            var filter = Builders<T>.Filter.Where(x => ids.Contains(x.Id));
            return _collection.Find(filter).ToEnumerable();
        }

        public IEnumerable<T> Find(FilterDefinition<T> filter) => _collection.Find(filter).ToEnumerable();

        public bool Update(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition)
        {
            var count = _collection.UpdateMany(filter, updateDefinition);

            return count.ModifiedCount != 0;
        }

        public bool Delete(FilterDefinition<T> filter) => _collection.DeleteMany(filter).DeletedCount != 0;
    }
}
