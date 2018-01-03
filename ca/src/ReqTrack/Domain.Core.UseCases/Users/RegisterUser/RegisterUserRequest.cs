using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Users.RegisterUser
{
    public class RegisterUserRequest : RequestModel
    {
        public RegisterUserRequest() : base(null)
        {
            //This is a unauthorized request, so no user can be the requester.
        }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (!UserValidationHelper.IsUserNameValid(UserName))
            {
                errors.Add(nameof(UserName), "Username is invalid");
            }

            if (!UserValidationHelper.IsDisplayNameValid(DisplayName))
            {
                errors.Add(nameof(DisplayName), "Display name is invalid");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                errors.Add(nameof(Password), "Password can't be empty");
            }

            return !errors.Any();
        }
    }
}
