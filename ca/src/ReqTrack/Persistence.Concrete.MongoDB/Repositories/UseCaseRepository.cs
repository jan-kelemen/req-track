using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    internal class UseCaseRepository : BaseRepository, IUseCaseRepository
    {
        public UseCaseRepository(MongoReqTrackDatabase database) : base(database)
        {
        }

        public Identity CreateUseCase(UseCase useCase)
        {
            var mongoUseCase = useCase.ToMongoEntity();

            if (_projectRepository.Count(_projectRepository.IdFilter(mongoUseCase.ProjectId)) == 0)
            {
                throw new EntityNotFoundException($"ID={useCase.Project.Id}");
            }

            if (_userRepository.Count(_userRepository.IdFilter(mongoUseCase.AuthorId)) == 0)
            {
                throw new EntityNotFoundException($"ID={useCase.Author.Id}");
            }

            mongoUseCase.OrderMarker = LastOrderMarker(mongoUseCase.ProjectId) + 1;

            return _useCaseRepository.Create(mongoUseCase).ToDomainIdentity();
        }

        public UseCase ReadUseCase(Identity id)
        {
            var useCase = _useCaseRepository.Read(id.ToMongoIdentity());
            if (useCase == null) { throw new EntityNotFoundException($"ID={id}"); }

            var project = _projectRepository.Read(useCase.ProjectId);
            if (project == null) { throw new EntityNotFoundException($"ID={useCase.ProjectId}"); }

            var user = _userRepository.Read(useCase.AuthorId);
            if (user == null)
            {
                //try to atleast get a project author, as author of the useCase
                var projectAuthor = _userRepository.Read(project.AuthorId);
                if (projectAuthor == null)
                {
                    throw new EntityNotFoundException($"ID={useCase.AuthorId}, {project.AuthorId}");
                }
            }

            return useCase.ToDomainEntity(user, project);
        }

        public bool UpdateUseCase(UseCase useCase)
        {
            var mongoUseCase = useCase.ToMongoEntity();

            if (_projectRepository.Count(_projectRepository.IdFilter(mongoUseCase.ProjectId)) == 0)
            {
                throw new EntityNotFoundException($"ID={useCase.Project.Id}");
            }

            if (_userRepository.Count(_userRepository.IdFilter(mongoUseCase.AuthorId)) == 0)
            {
                throw new EntityNotFoundException($"ID={useCase.Author.Id}");
            }

            var orderMarker = LastOrderMarker(mongoUseCase.ProjectId) + 1;

            var useCaseFilter = _useCaseRepository.IdFilter(mongoUseCase.Id);
            var updateDefinition = Builders<MongoUseCase>.Update
                .Set(x => x.AuthorId, mongoUseCase.AuthorId)
                .Set(x => x.Title, mongoUseCase.Title)
                .Set(x => x.Note, mongoUseCase.Note)
                .Set(x => x.Steps, mongoUseCase.Steps)
                .Set(x => x.OrderMarker, orderMarker);

            return _useCaseRepository.Update(useCaseFilter, updateDefinition);
        }

        public bool DeleteUseCase(Identity id)
        {
            return _useCaseRepository.Delete(_useCaseRepository.IdFilter(id.ToMongoIdentity()));
        }

        private int LastOrderMarker(ObjectId projectId)
        {
            var projectFilter = Builders<MongoUseCase>.Filter.Eq(x => x.ProjectId, projectId);
            var lastUseCase = _useCaseRepository
                .Find(projectFilter)
                .OrderByDescending(x => x.OrderMarker) //https://stackoverflow.com/a/41503983 perfectly fine
                .FirstOrDefault();

            return lastUseCase?.OrderMarker ?? -1;
        }
    }
}
