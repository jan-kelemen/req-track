using MongoDB.Driver;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Database
{
    internal class MongoReqTrackDatabase
    {
        private readonly IMongoClient _client;

        private readonly IMongoDatabase _database;

        public MongoReqTrackDatabase()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("ReqTrack");
        }

        public IMongoCollection<MongoProject> ProjectCollection => _database.GetCollection<MongoProject>(MongoProject.CollectionName);

        public IMongoCollection<MongoRequirement> RequirementCollection => _database.GetCollection<MongoRequirement>(MongoRequirement.CollectionName);
    }
}
