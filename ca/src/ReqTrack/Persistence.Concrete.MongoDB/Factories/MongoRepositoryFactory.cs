using ReqTrack.Domain.Core.Repositories.Factories;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;

namespace ReqTrack.Persistence.Concrete.MongoDB.Factories
{
    internal class MongoRepositoryFactory : IRepositoryFactory
    {
        private MongoReqTrackDatabase _database;

        public MongoRepositoryFactory(MongoReqTrackDatabase database)
        {
            _database = database;
        }

        public IProjectRepository ProjectRepository => new MongoProjectRepository(_database);

        public IRequirementRepository RequirementRepository => new MongoRequirementRepository(_database);
    }
}
