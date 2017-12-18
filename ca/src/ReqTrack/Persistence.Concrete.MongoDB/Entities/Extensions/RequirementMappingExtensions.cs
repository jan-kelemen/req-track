using ReqTrack.Domain.Core.Entities.Requirements;
using System;

namespace ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions
{
    internal static class RequirementMappingExtensions
    {
        public static MongoRequirement ToMongoEntity(this Requirement requirement)
        {
            return new MongoRequirement
            {
                Id = requirement.Id.ToMongoIdentity(),
                Type = requirement.Type.ToString(),
                Title = requirement.Title,
                Details = requirement.Details,
                ProjectId = requirement.ProjectId.ToMongoIdentity(),
            };
        }

        public static Requirement ToDomainEntity(this MongoRequirement requirement)
        {
            return new Requirement(
                requirement.Id.ToDomainIdentity(),
                requirement.Title,
                Enum.Parse<RequirementType>(requirement.Type),
                requirement.Details,
                requirement.ProjectId.ToDomainIdentity()
                );
        }
    }
}
