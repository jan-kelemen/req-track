using MongoDB.Driver;

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
    }
}
