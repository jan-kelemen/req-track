using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangePassword
{
    public class ChangePasswordResponse : ResponseModel
    {
        internal ChangePasswordResponse() : base(ExecutionStatus.Success)
        {
        }

        public string UserId { get; set; }

        public string DisplayName { get; set; }

        //Password fields are cleared
    }
}
