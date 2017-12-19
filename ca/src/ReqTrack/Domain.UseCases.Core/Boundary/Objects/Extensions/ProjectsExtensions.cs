using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using System;
using System.Linq;

using DProjectInfo = ReqTrack.Domain.Core.Entities.Projects.ProjectInfo;
using DProjectWithRequirements = ReqTrack.Domain.Core.Entities.Projects.ProjectWithRequirements;
using DRequirement = ReqTrack.Domain.Core.Entities.Projects.ProjectWithRequirements.Requirement;
using DRequirementType = ReqTrack.Domain.Core.Entities.Requirements.RequirementType;

namespace ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions
{
    public static class ProjectsExtensions
    {
        /// <summary>
        /// Converts the boundary object to domain entity.
        /// </summary>
        /// <param name="projectInfo">Boundary object project info.</param>
        /// <param name="identifier">Identifer to use if the boundar objects identity is <c>null</c>.</param>
        /// <returns>Domain entity.</returns>
        public static DProjectInfo ToDomainEntity(this ProjectInfo projectInfo, string identifier = null)
        {
            var id = projectInfo.Id.ToDomainIdentity(identifier);
            return new DProjectInfo(id, projectInfo.Name);
        }

        /// <summary>
        /// Converts the domain object to boundary object.
        /// </summary>
        /// <param name="projectInfo">Domain entity project info.</param>
        /// <returns>Boundary object.</returns>
        public static ProjectInfo ToBoundaryObject(this DProjectInfo projectInfo) => new ProjectInfo
        {
            Id = projectInfo.Id.ToBoundaryIdentity(),
            Name = projectInfo.Name,
        };

        public static DProjectWithRequirements ToDomainEntity(this ProjectWithRequirements project) => new DProjectWithRequirements(
            project.Id.ToDomainIdentity(),
            project.Name,
            project.Requirements.Select(r => r.ToDomainEntity()));

        public static ProjectWithRequirements ToBoundaryObject(this DProjectWithRequirements project) => new ProjectWithRequirements
        {
            Id = project.Id.ToBoundaryIdentity(),
            Name = project.Name,
            Requirements = project.Requirements.Select(r => r.ToBoundaryObject()),
        };

        private static DRequirement ToDomainEntity(this ProjectWithRequirements.Requirement requirement) => new DRequirement(
            requirement.Id.ToDomainIdentity(),
            Enum.Parse<DRequirementType>(requirement.Type),
            requirement.Title);

        private static ProjectWithRequirements.Requirement ToBoundaryObject(this DRequirement requirement) => new ProjectWithRequirements.Requirement
        {
            Id = requirement.Id.ToBoundaryIdentity(),
            Title = requirement.Title,
            Type = requirement.Type.ToString()
        };
    }
}
