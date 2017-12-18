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
}
