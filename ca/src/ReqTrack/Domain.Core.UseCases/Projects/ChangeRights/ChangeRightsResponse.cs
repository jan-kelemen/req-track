using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRights
{
    public class ChangeRightsResponse : ResponseModel
    {
        internal ChangeRightsResponse() : base(ExecutionStatus.Success)
        {
        }

        public string ProjectId { get; set; }

        public IEnumerable<ProjectRights> Rights { get; set; }
    }
}
