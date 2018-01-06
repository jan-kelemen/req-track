using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangePassword
{
    public class ChangePasswordResponse : ResponseModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
