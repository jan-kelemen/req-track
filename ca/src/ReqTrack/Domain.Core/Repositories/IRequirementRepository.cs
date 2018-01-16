using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IRequirementRepository
    {
        Identity CreateRequirement(Requirement requirement);

        Requirement ReadRequirement(Identity id);

        bool UpdateRequirement(Requirement requirement);

        bool DeleteRequirement(Identity id);
    }
}
