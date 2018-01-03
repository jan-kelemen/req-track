using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Users.RegisterUser
{
    public class RegisterUserResponse : ResponseModel
    {
        internal RegisterUserResponse(ExecutionStatus status) : base(status)
        {
        }

        public string GivenId { get; set; }
    }
}
