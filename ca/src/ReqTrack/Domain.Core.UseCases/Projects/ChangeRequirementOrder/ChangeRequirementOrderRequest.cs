using System.Collections.Generic;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder
{
    public class ChangeRequirementOrderRequest : ChangeRequirementOrderInitialRequest
    {

        public ChangeRequirementOrderRequest(string requestedBy) : base(requestedBy)
        {
        }

        public IEnumerable<Requirement> Requirements { get; set; }
    }
}
