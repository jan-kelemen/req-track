using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.DeleteRequirement
{
    public class DeleteRequirementResponse : ResponseModel
    {
        internal DeleteRequirementResponse() : base(ExecutionStatus.Success)
        {
        }
    }
}
