using ReqTrack.Domain.Core.Repositories.Factories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Factories;

namespace ReqTrack.Persistence.Concrete.MongoDB.Configuration
{
    public class Initializer
    {
        public IRepositoryFactory Initialize(string url = "mongodb://localhost:27017")
        {
            return new MongoRepositoryFactory(new MongoReqTrackDatabase());
        }
    }
}
