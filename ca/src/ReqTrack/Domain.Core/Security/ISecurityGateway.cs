﻿using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Domain.Core.Security
{
    public interface ISecurityGateway
    {
        IEnumerable<ProjectRights> GetProjectRights(Identity projectId);

        ProjectRights GetProjectRights(Identity projectId, Identity userId);

        bool ChangeProjectRights(Identity projectId, IEnumerable<ProjectRights> newProjectRights);
    }
}
