using System.Collections.Generic;
using System.Linq;
namespace ReqTrack.Domain.Core.UseCases.Users.ChangePassword
{
    public class ChangePasswordRequest : ChangePasswordInitialRequest
    {
        public ChangePasswordRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmedPassword { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (string.IsNullOrWhiteSpace(OldPassword))
            {
                errors.Add(nameof(OldPassword), "Password field is empty");
            }

            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                errors.Add(nameof(NewPassword), "Password field is empty");
            }

            if (string.IsNullOrWhiteSpace(ConfirmedPassword))
            {
                errors.Add(nameof(ConfirmedPassword), "Password field is empty");
            }

            if (NewPassword != ConfirmedPassword)
            {
                errors.Add("", "New and confirmed passwords don't match");
            }
        }
    }
}
