using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects
{
    /// <summary>
    /// Boundary object for basic project information, <see cref="Domain.Core.Entities.Project.ProjectInfo"/>.
    /// </summary>
    public class ProjectInfo
    {
        /// <summary>
        /// Identity of the project, can be set to null or empty if it's used for creation of the project, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the project, <see cref="Domain.Core.Entities.Project.ProjectInfo.Name"/>.
        /// </summary>
        public string Name { get; set; }
    }

    public static class ProjectInfoExtensions
    {
        /// <summary>
        /// Converts the boundary object to domain entity.
        /// </summary>
        /// <param name="projectInfo">Boundary object project info.</param>
        /// <returns>Domain entity.</returns>
        public static Domain.Core.Entities.Projects.ProjectInfo ConvertToDomainEntity(this ProjectInfo projectInfo, string identifier = null)
        {
            var id = string.IsNullOrEmpty(projectInfo.Id) 
                ? (string.IsNullOrEmpty(identifier) ? Identity.BlankIdentity : Identity.FromString(identifier))
                : Identity.FromString(projectInfo.Id);

            var rv = new Domain.Core.Entities.Projects.ProjectInfo(id, projectInfo.Name);

            return rv;
        }

        /// <summary>
        /// Converts the domain object to boundary object.
        /// </summary>
        /// <param name="projectInfo">Domain entity project info.</param>
        /// <returns>Boundary object.</returns>
        public static ProjectInfo ConvertToBoundaryEntity(this Domain.Core.Entities.Projects.ProjectInfo projectInfo)
        {
            return new ProjectInfo
            {
                Id = projectInfo.Id.ToString(),
                Name = projectInfo.Name,
            };
        }
    }
}
