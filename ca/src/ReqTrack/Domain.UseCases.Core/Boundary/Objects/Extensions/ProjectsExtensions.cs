﻿using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

using DProjectInfo = ReqTrack.Domain.Core.Entities.Projects.ProjectInfo;

namespace ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions
{
    public static class ProjectsExtensions
    {
        /// <summary>
        /// Converts the boundary object to domain entity.
        /// </summary>
        /// <param name="projectInfo">Boundary object project info.</param>
        /// <returns>Domain entity.</returns>
        public static DProjectInfo ConvertToDomainEntity(this ProjectInfo projectInfo, string identifier = null)
        {
            var id = string.IsNullOrEmpty(projectInfo.Id) 
                ? (string.IsNullOrEmpty(identifier) ? Identity.BlankIdentity : Identity.FromString(identifier))
                : Identity.FromString(projectInfo.Id);

            var rv = new DProjectInfo(id, projectInfo.Name);

            return rv;
        }

        /// <summary>
        /// Converts the domain object to boundary object.
        /// </summary>
        /// <param name="projectInfo">Domain entity project info.</param>
        /// <returns>Boundary object.</returns>
        public static ProjectInfo ConvertToBoundaryEntity(this DProjectInfo projectInfo)
        {
            return new ProjectInfo
            {
                Id = projectInfo.Id.ToString(),
                Name = projectInfo.Name,
            };
        }
    }
}
