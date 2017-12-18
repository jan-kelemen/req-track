using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Project;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ReqTrack.Domain.Core.Entities.Requirement;
using System;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions
{
    internal static class ProjectMappingExtensions
    {
        public static Project ToMongoEntity(this ProjectInfo projectInfo)
        {
            return new Project
            {
                Id = projectInfo.Id.IsBlankIdentity() ? ObjectId.GenerateNewId() : ObjectId.Parse(projectInfo.Id.ToString()),
                Name = projectInfo.Name,
            };
        }

        public static ProjectInfo ToDomainEntity(this Project project)
        {
            return new ProjectInfo(Identity.FromString(project.Id.ToString()), project.Name);
        }

        public static Project ToMongoEntity(this ProjectWithRequirements projectWithRequirements)
        {
            var project = ToMongoEntity(projectWithRequirements as ProjectInfo);
            project.Requirements = projectWithRequirements.Requirements.Select(r => new Project.Requirement
            {
                Id = ObjectId.Parse(r.Id.ToString()),
                Type = r.Type.ToString(),
                OrderMarker = r.OrderMarker,
            });
            return project;
        }

        public static ProjectWithRequirements ToDomainEntity(this Project project, Dictionary<ObjectId, Entities.Requirement> requirements)
        {
            return new ProjectWithRequirements(
                Identity.FromString(project.Id.ToString()),
                project.Name,
                project.Requirements.Select(
                    r => new ProjectWithRequirements.Requirement(
                        Identity.FromString(r.Id.ToString()),
                        Enum.Parse<RequirementType>(r.Type),
                        requirements[r.Id].Title,
                        r.OrderMarker)
                        )
                    );
        }
    }
}
