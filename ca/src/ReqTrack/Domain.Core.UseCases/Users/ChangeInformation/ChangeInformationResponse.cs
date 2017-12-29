using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangeInformation
{
    public class ChangeInformationResponse : ResponseModel
    {
        internal ChangeInformationResponse(ExecutionStatus status) : base(status)
        {
        }

        public string UserId { get; set; }

        public string DisplayName { get; set; }
    }
}
