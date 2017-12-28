using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping
{
    public static class ProjectMappingExtensions
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

        //TODO: handle usecases
        public static Project ToDomainEntity(
            this MongoProject project, 
            MongoUser author, 
            IEnumerable<MongoRequirement> requirements = null)
        {
            var reqs = requirements?.Select(r =>
                new Project.Requirement(
                    r.Id.ToDomainIdentity(),
                    Enum.Parse<RequirementType>(r.Type),
                    r.Title,
                    r.OrderMarker)
            );

            var reqsObject = reqs == null ? null : new ProjectRequirements(reqs);

            return new Project(
                project.Id.ToDomainIdentity(),
                author.ToDomainEntity(),
                project.Name,
                project.Description,
                reqsObject);
        }
    }
}
