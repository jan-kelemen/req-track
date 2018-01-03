using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangeInformation
{
    public class ChangeInformationResponse : ResponseModel
    {
        internal ChangeInformationResponse() : base(ExecutionStatus.Success)
        {
        }

        public string UserId { get; set; }

        public string DisplayName { get; set; }
    }
}
