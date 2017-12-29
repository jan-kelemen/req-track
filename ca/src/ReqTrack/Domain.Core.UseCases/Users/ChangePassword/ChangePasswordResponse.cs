using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangePassword
{
    public class ChangePasswordResponse : ResponseModel
    {
        internal ChangePasswordResponse(ExecutionStatus status) : base(status)
        {
        }

        public string UserId { get; set; }

        //Password fields are cleared
    }
}
