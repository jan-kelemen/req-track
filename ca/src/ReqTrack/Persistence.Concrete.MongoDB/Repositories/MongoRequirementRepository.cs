using System;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    public class MongoRequirementRepository : MongoBaseRepository, IRequirementRepository
    {
        public CreateResult<Requirement> CreateRequirement(Requirement requirement)
        {
            throw new NotImplementedException();
        }

        public ReadResult<Requirement> ReadRequirement(Identity id)
        {
            throw new NotImplementedException();
        }

        public UpdateResult<Requirement> UpdateRequirement(Requirement requirement)
        {
            throw new NotImplementedException();
        }

        public DeleteResult<Identity> DeleteRequirement(Identity id)
        {
            throw new NotImplementedException();
        }
    }
}
