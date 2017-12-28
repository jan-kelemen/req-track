using MongoDB.Driver;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Database
{
    public class MongoReqTrackDatabase
    {
        private readonly IMongoClient _client;

        private readonly IMongoDatabase _database;

        public MongoReqTrackDatabase(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("ReqTrack");
        }

        public IMongoCollection<MongoUser> UserCollection => 
            _database.GetCollection<MongoUser>(MongoUser.CollectionName);

        public IMongoCollection<MongoProject> ProjectCollection =>
            _database.GetCollection<MongoProject>(MongoProject.CollectionName);

        public IMongoCollection<MongoRequirement> RequirementCollection =>
            _database.GetCollection<MongoRequirement>(MongoRequirement.CollectionName);

        public IMongoCollection<MongoUseCase> UseCaseCollection =>
            _database.GetCollection<MongoUseCase>(MongoUseCase.CollectionName);
    }
}
