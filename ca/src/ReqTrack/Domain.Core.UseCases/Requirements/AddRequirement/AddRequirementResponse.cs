using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement
{
    public class AddRequirementResponse : ResponseModel
    {
        public AddRequirementResponse() : base(ExecutionStatus.Success)
        {
        }

        public string GivenId { get; set; }
    }
}
