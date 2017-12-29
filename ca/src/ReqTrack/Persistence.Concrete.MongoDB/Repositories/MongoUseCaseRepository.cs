using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    public class MongoUseCaseRepository : MongoBaseRepository, IUseCaseRepository
    {
        private readonly IMongoCollection<MongoUseCase> _useCases;

        private readonly IMongoCollection<MongoProject> _projects;

        private readonly IMongoCollection<MongoUser> _users;

        public MongoUseCaseRepository(
            IMongoCollection<MongoUseCase> useCases,
            IMongoCollection<MongoProject> projects,
            IMongoCollection<MongoUser> users)
        {
            _useCases = useCases;
            _projects = projects;
            _users = users;
        }

        public Identity CreateUseCase(UseCase useCase)
        {
            var mongoUseCase = useCase.ToMongoEntity();

            var project = FindByIdOrThrow(_projects, mongoUseCase.ProjectId);
            var user = FindByIdOrThrow(_users, mongoUseCase.AuthorId);

            mongoUseCase.OrderMarker = LastOrderMarker(project.Id) + 1;

            _useCases.InsertOne(mongoUseCase);

            return mongoUseCase.Id.ToDomainIdentity();
        }

        public UseCase ReadUseCase(Identity id)
        {
            var mongoUseCase = FindByIdOrThrow(_useCases, id.ToMongoIdentity());
            var mongoProject = FindByIdOrThrow(_projects, mongoUseCase.ProjectId);
            var mongoUser = FindByIdOrThrow(_users, mongoUseCase.AuthorId);

            return mongoUseCase.ToDomainEntity(mongoUser, mongoProject);
        }

        public bool UpdateUseCase(UseCase useCase)
        {
            var mongoUseCase = useCase.ToMongoEntity();

            var mongoProject = FindByIdOrThrow(_projects, mongoUseCase.ProjectId);
            var mongoUser = FindByIdOrThrow(_users, mongoUseCase.AuthorId);
            var orderMarker = LastOrderMarker(mongoProject.Id) + 1;

            var useCaseFilter = Builders<MongoUseCase>.Filter.Eq(x => x.Id, mongoUseCase.Id);
            var updateDefinition = Builders<MongoUseCase>.Update
                .Set(x => x.AuthorId, mongoUser.Id)
                .Set(x => x.Title, mongoUseCase.Title)
                .Set(x => x.Note, mongoUseCase.Note)
                .Set(x => x.Steps, mongoUseCase.Steps)
                .Set(x => x.OrderMarker, orderMarker);
            var options = new FindOneAndUpdateOptions<MongoUseCase>
            {
                ReturnDocument = ReturnDocument.After,
            };

            var updated = _useCases.FindOneAndUpdate(useCaseFilter, updateDefinition, options);

            return true;
        }

        public bool DeleteUseCase(Identity id)
        {
            var filter = Builders<MongoUseCase>.Filter.Eq(x => x.Id, id.ToMongoIdentity());
            var entity = _useCases.FindOneAndDelete(filter);
            return true;
        }

        private int LastOrderMarker(ObjectId projectId)
        {
            var projectFilter = Builders<MongoUseCase>.Filter.Eq(x => x.ProjectId, projectId);
            var lastRequirement = _useCases
                .Find(projectFilter)
                .SortByDescending(x => x.OrderMarker) //https://stackoverflow.com/a/41503983 perfectly fine
                .Limit(1)
                .FirstOrDefault();

            return lastRequirement?.OrderMarker ?? -1;
        }
    }
}
