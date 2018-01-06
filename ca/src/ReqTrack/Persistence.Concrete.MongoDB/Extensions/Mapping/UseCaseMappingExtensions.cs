using System.Linq;
using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping
{
    internal static class UseCaseMappingExtensions
    {
        public static MongoUseCase ToMongoEntity(this UseCase useCase)
        {
            return new MongoUseCase
            {
                Id = useCase.Id.IsBlankIdentity() ? ObjectId.GenerateNewId() : useCase.Id.ToMongoIdentity(),
                AuthorId = useCase.Author.Id.ToMongoIdentity(),
                ProjectId = useCase.Project.Id.ToMongoIdentity(),
                Title = useCase.Title,
                Note = useCase.Note,
                Steps = useCase.Steps.Select(x => x.Content),
            };
        }

        public static UseCase ToDomainEntity(this MongoUseCase useCase, MongoUser author, MongoProject project)
        {
            return new UseCase(
                useCase.Id.ToDomainIdentity(),
                new BasicProject(project.Id.ToDomainIdentity(), project.Name),
                new BasicUser(author.Id.ToDomainIdentity(), author.DisplayName),
                useCase.Title,
                useCase.Note,
                useCase.Steps.Select(x => new UseCase.UseCaseStep { Content = x })); //OrderMarker probably isnt needed
        }
    }
}
