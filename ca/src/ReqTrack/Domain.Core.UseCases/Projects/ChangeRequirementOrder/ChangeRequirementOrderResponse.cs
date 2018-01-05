using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder
{
    public class ChangeRequirementOrderResponse : ResponseModel
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }
    }
}
