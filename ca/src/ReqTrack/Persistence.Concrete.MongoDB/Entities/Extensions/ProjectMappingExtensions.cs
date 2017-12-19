using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities.Projects;
using System.Linq;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.Requirements;
using System;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions
{
    internal static class ProjectMappingExtensions
    {
        public static MongoProject ToMongoEntity(this ProjectInfo projectInfo) => new MongoProject
        {
            Id = projectInfo.Id.ToMongoIdentity(),
            Name = projectInfo.Name,
        };

        public static ProjectInfo ToDomainEntity(this MongoProject project) => new ProjectInfo(project.Id.ToDomainIdentity(), project.Name);

        public static MongoProject ToMongoEntity(this ProjectWithRequirements projectWithRequirements)
        {
            var project = ToMongoEntity(projectWithRequirements as ProjectInfo);
            project.RequirementIds = projectWithRequirements.Requirements.Select(r => r.Id.ToMongoIdentity());
            return project;
        }

        public static ProjectWithRequirements ToDomainEntity(this MongoProject project, Dictionary<ObjectId, MongoRequirement> requirements) => new ProjectWithRequirements(
            project.Id.ToDomainIdentity(),
            project.Name,
            project.RequirementIds.Select(
                id => new ProjectWithRequirements.Requirement(
                    id.ToDomainIdentity(),
                    Enum.Parse<RequirementType>(requirements[id].Type),
                    requirements[id].Title)
            )
        );
    }
}
