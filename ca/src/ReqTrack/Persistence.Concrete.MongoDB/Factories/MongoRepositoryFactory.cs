using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories;

namespace ReqTrack.Persistence.Concrete.MongoDB.Factories
{
    internal class MongoRepositoryFactory : IRepositoryFactory
    {
        public MongoRepositoryFactory(MongoReqTrackDatabase database)
        {
            UserRepository = new UserRepository(database);
            ProjectRepository = new ProjectRepository(database);
            RequirementRepository = new RequirementRepository(database);
            UseCaseRepository = new UseCaseRepository(database);
        }

        public IUserRepository UserRepository { get; }

        public IProjectRepository ProjectRepository { get; }

        public IRequirementRepository RequirementRepository { get; }

        public IUseCaseRepository UseCaseRepository { get; }
    }
}
