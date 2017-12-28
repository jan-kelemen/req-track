using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Security;

namespace ReqTrack.Persistence.Concrete.MongoDB.Security
{
    public class MongoSecurityGateway : ISecurityGateway
    {
        public IEnumerable<ProjectRights> GetAllProjectRights(Identity projectId)
        {
            throw new NotImplementedException();
        }

        public ProjectRights GetProjectRights(Identity projectId, Identity userId)
        {
            throw new NotImplementedException();
        }
    }
}
