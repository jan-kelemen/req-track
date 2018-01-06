using System;
using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping
{
    internal static class RequirementMapingExtensions
    {
        public static MongoRequirement ToMongoEntity(this Requirement requirement, int orderMarker = int.MaxValue)
        {
            return new MongoRequirement
            {
                Id = requirement.Id.IsBlankIdentity() ? ObjectId.GenerateNewId() : requirement.Id.ToMongoIdentity(),
                AuthorId = requirement.Author.Id.ToMongoIdentity(),
                ProjectId = requirement.Project.Id.ToMongoIdentity(),
                Type = requirement.Type.ToString(),
                Title = requirement.Title,
                Note = requirement.Note,
                OrderMarker = orderMarker,
            };
        }

        public static Requirement ToDomainEntity(this MongoRequirement requirement, MongoUser author, MongoProject project)
        {
            return new Requirement(
                requirement.Id.ToDomainIdentity(),
                new BasicProject(project.Id.ToDomainIdentity(), project.Name),
                new BasicUser(author.Id.ToDomainIdentity(), author.DisplayName),
                Enum.Parse<RequirementType>(requirement.Type),
                requirement.Title,
                requirement.Note
            );
        }
    }
}
