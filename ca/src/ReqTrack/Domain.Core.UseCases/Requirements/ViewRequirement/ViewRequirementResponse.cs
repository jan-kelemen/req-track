using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement
{
    public class ViewRequirementResponse : ResponseModel
    {
        public ViewRequirementResponse() : base(ExecutionStatus.Success)
        {
        }

        public string RequirementId { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }

        public Project Project { get; set; }

        public User Author { get; set; }
    }
}
