using System;
using System.Collections.Generic;
using System.Text;
using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRights
{
    public class ChangeRightsResponse : ResponseModel
    {
        internal ChangeRightsResponse(ExecutionStatus status) : base(status)
        {
        }

        public string ProjectId { get; set; }

        public IEnumerable<ProjectRights> Rights { get; set; }
    }
}
