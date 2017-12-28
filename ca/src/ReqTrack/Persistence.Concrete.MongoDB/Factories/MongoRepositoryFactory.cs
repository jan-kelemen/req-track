using MongoDB.Driver;
using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;

namespace ReqTrack.Persistence.Concrete.MongoDB.Factories
{
    public class MongoRepositoryFactory : IRepositoryFactory
    {
        private readonly MongoReqTrackDatabase _database;

        public MongoRepositoryFactory(MongoReqTrackDatabase database)
        {
            _database = database;
        }

        public IUserRepository UserRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IRequirementRepository RequirementRepository { get; }
        public IUseCaseRepository UseCaseRepository { get; }
    }
}
