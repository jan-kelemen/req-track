using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Users.DeleteUser
{
    public class DeleteUserResponse : ResponseModel
    {
        internal DeleteUserResponse() : base(ExecutionStatus.Success)
        {
        }

        public string UserId { get; set; }
    }
}
