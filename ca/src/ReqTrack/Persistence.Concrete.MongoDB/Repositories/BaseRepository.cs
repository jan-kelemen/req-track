using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class BaseRepository : IRepository
    {
        protected readonly MongoRepository<MongoUser> _userRepository;

        protected readonly MongoRepository<MongoProject> _projectRepository;

        protected readonly MongoRepository<MongoRequirement> _requirementRepository;

        protected readonly MongoRepository<MongoUseCase> _useCaseRepository;

        public BaseRepository(MongoReqTrackDatabase database)
        {
            _userRepository = new MongoRepository<MongoUser>(database.UserCollection);
            _projectRepository = new MongoRepository<MongoProject>(database.ProjectCollection);
            _requirementRepository = new MongoRepository<MongoRequirement>(database.RequirementCollection);
            _useCaseRepository = new MongoRepository<MongoUseCase>(database.UseCaseCollection);
        }

        public Identity GenerateNewIdentity() => Identity.FromString(ObjectId.GenerateNewId().ToString());
    }
}