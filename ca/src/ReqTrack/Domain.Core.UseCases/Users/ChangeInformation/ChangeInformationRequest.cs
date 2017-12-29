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

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (UserValidationHelper.IsDisplayNameValid(DisplayName))
            {
                errors.Add(nameof(DisplayName), "Display name is invalid");
            }

            return !errors.Any();
        }
    }
}
