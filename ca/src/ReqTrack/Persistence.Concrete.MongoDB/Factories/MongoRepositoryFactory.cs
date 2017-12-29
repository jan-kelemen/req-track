using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal;

namespace ReqTrack.Persistence.Concrete.MongoDB.Factories
{
    public class MongoRepositoryFactory : IRepositoryFactory
    {
        private readonly MongoReqTrackDatabase _database;

        public MongoRepositoryFactory(MongoReqTrackDatabase database) => _database = database;

        public IUserRepository UserRepository => 
            new MongoUserRepository(
                _database.UserCollection, 
                _database.ProjectCollection,
                _database.RequirementCollection,
                _database.UseCaseCollection);

        public IProjectRepository ProjectRepository =>
            new MongoProjectRepository(
                _database.ProjectCollection,
                _database.RequirementCollection,
                _database.UserCollection,
                _database.UseCaseCollection
            );

        public IRequirementRepository RequirementRepository => 
            new MongoRequirementRepository(
                _database.RequirementCollection,
                _database.ProjectCollection,
                _database.UserCollection);

        public IUseCaseRepository UseCaseRepository => 
            new MongoUseCaseRepository(
                _database.UseCaseCollection,
                _database.ProjectCollection,
                _database.UserCollection);
    }
}
