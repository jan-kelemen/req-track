using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser
{
    public class AuthorizeUserResponse : ResponseModel
    {
        public AuthorizeUserResponse() : base(ExecutionStatus.Success)
        {
        }

        public string UserId { get; set; }

        public string DisplayName { get; set; }
    }
}
