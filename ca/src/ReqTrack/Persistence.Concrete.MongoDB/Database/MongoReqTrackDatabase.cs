using MongoDB.Driver;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Database
{
    internal class MongoReqTrackDatabase
    {
        private IMongoClient _client;

        private IMongoDatabase _database;

        public MongoReqTrackDatabase()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("ReqTrack");
        }

        public IMongoCollection<Project> ProjectCollection => _database.GetCollection<Project>(Project.CollectionName);

        public IMongoCollection<Requirement> RequirementCollection => _database.GetCollection<Requirement>(Requirement.CollectionName);
    }
}
