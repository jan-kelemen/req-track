using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Users.DeleteUser
{
    public class DeleteUserResponse : ResponseModel
    {
        internal DeleteUserResponse(ExecutionStatus status) : base(status)
        {
        }

        public string UserId { get; set; }
    }
}
