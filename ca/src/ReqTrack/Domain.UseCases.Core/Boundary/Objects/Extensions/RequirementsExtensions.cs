using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;
using System;

using DRequirement = ReqTrack.Domain.Core.Entities.Requirements.Requirement;
using DRequirementType = ReqTrack.Domain.Core.Entities.Requirements.RequirementType;

namespace ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions
{
    public static class RequirementsExtensions
    {
        public static DRequirement ToDomainEntity(this Requirement requirement, string identifier = null)
        {
            var id = requirement.Id.ToDomainIdentity();
            var projectId = requirement.ProjectId.ToDomainIdentity();
            return new DRequirement(id,
                requirement.Title,
                Enum.Parse<DRequirementType>(requirement.Type),
                requirement.Details,
                projectId);
        }

        public static Requirement ToBoundaryObject(this DRequirement requirement) => new Requirement
        {
            Id = requirement.Id.ToBoundaryIdentity(),
            Title = requirement.Title,
            Type = requirement.Type.ToString(),
            Details = requirement.Details,
            ProjectId = requirement.ProjectId.ToBoundaryIdentity(),
        };
    }
}
