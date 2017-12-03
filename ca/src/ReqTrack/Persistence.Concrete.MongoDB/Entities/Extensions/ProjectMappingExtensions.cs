using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Project;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
