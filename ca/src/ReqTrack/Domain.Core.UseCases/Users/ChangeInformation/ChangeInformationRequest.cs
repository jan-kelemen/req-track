using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
namespace ReqTrack.Domain.Core.UseCases.Users.ChangeInformation
{
    public class ChangeInformationRequest : ChangeInformationInitialRequest
    {
        public ChangeInformationRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string DisplayName { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (UserValidationHelper.IsDisplayNameValid(DisplayName))
            {
                errors.Add(nameof(DisplayName), "Display name is invalid");
            }
        }
    }
}
