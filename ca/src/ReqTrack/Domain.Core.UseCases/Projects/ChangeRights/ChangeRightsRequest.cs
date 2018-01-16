using System.Collections.Generic;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRights
{
    public class ChangeRightsRequest : ChangeRightsInitialRequest
    {
        public ChangeRightsRequest(string requestedBy) : base(requestedBy)
        {
        }

        public IEnumerable<ProjectRights> Rights { get; set; }
    }
}
