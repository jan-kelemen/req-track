using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Users.RegisterUser
{
    public class RegisterUserResponse : ResponseModel
    {
        internal RegisterUserResponse(ExecutionStatus status) : base(status)
        {
        }

        public string GivenId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        //Password is cleared on response.
    }
}
