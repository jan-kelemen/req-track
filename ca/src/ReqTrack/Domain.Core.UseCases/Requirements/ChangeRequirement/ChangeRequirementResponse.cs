using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement
{
    public class ChangeRequirementResponse : ResponseModel
    {
        public ChangeRequirementResponse() : base(ExecutionStatus.Success)
        {
        }

        public string ProjectId { get; set; }

        public string RequirementId { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }
    }
}
