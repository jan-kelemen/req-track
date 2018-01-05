using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser
{
    public class AuthorizeUserResponse : ResponseModel
    {
        public string UserId { get; set; }

        public string DisplayName { get; set; }
    }
}
