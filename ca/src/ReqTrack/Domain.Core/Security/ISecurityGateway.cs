using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Domain.Core.Security
{
    public interface ISecurityGateway
    {
        IEnumerable<ProjectRights> GetAllProjectRights(Identity projectId);

        ProjectRights GetProjectRights(Identity projectId, Identity userId);
    }
}
