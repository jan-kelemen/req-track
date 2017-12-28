using MongoDB.Driver;
using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories;

namespace ReqTrack.Persistence.Concrete.MongoDB.Factories
{
    public class MongoRepositoryFactory : IRepositoryFactory
    {
        private readonly MongoReqTrackDatabase _database;

        public MongoRepositoryFactory(MongoReqTrackDatabase database) => _database = database;

        public IUserRepository UserRepository => 
            new MongoUserRepository(_database.UserCollection, _database.ProjectCollection);

        public IProjectRepository ProjectRepository { get; }
        public IRequirementRepository RequirementRepository { get; }
        public IUseCaseRepository UseCaseRepository { get; }
    }
}
