using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;
namespace ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser
{
    public class AuthorizeUserRequest : RequestModel
    {
        public AuthorizeUserRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (!UserValidationHelper.IsUserNameValid(UserName))
            {
                errors.Add(nameof(UserName), "User name is invalid.");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                errors.Add(nameof(Password), "Password is empty");
            }
        }
    }
}
