using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping
{
    internal static class ProjectMappingExtensions
    {
        public static MongoProject ToMongoEntity(this Project project)
        {
            return new MongoProject
            {
                Id = project.Id.ToMongoIdentity(),
                AuthorId = project.Author.Id.ToMongoIdentity(),
                Name = project.Name,
                Description = project.Description,
            };
        }

        public static Project ToDomainEntity(
            this MongoProject project, 
            MongoUser author, 
            IEnumerable<MongoRequirement> requirements = null,
            IEnumerable<MongoUseCase> useCases = null)
        {
            var reqs = requirements?.Select(r =>
                new Project.Requirement(
                    r.Id.ToDomainIdentity(),
                    Enum.Parse<RequirementType>(r.Type),
                    r.Title,
                    r.OrderMarker)
            );

            var ucs = useCases?.Select(u =>
                new Project.UseCase(
                    u.Id.ToDomainIdentity(),
                    u.Title,
                    u.OrderMarker)
            );

            return new Project(
                project.Id.ToDomainIdentity(),
                new BasicUser(author.Id.ToDomainIdentity(), author.DisplayName), 
                project.Name,
                project.Description,
                reqs,
                ucs);
        }
    }
}
