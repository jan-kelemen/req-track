using System;
using System.Collections.Generic;
using System.Text;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IRequirementRepository
    {
        CreateResult<Requirement> CreateRequirement(Requirement requirement);

        ReadResult<Requirement> ReadRequirement(Identity id);

        UpdateResult<Requirement> UpdateRequirement(Requirement requirement);

        DeleteResult<Identity> DeleteRequirement(Identity id);
    }
}
