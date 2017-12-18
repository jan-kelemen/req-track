namespace ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements
{
    /// <summary>
    /// Boundary object for requierments, <see cref="Domain.Core.Entities.Requirements.Requirement"/>.
    /// </summary>
    public class Requirement
    {
        /// <summary>
        /// Identifier of the requierment, can be set to null or empty if it's used for creation of the project, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of the requirement, <see cref="Domain.Core.Entities.Requirements.Requirement.Title"/>.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Type of the requirement, <see cref="Domain.Core.Entities.Requirements.Requirement.Type"/>.
        /// </summary>
        /// <remarks><see cref="Domain.Core.Entities.Requirements.RequirementType"/> for possible requirement types.</remarks>
        public string Type { get; set; }

        /// <summary>
        /// Details of the requirement, <see cref="Domain.Core.Entities.Requirements.Requirement.Details"/>
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Identifier of the project to which the requirement belongs <see cref="Domain.Core.Entities.Requirements.Requirement.ProjectId"/>.
        /// </summary>
        public string ProjectId { get; set; }
    }
}
