using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation
{
    public class ChangeInformationRequest : ChangeInformationInitialRequest
    {
        public ChangeInformationRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (!ProjectValidationHelper.IsNameValid(Name))
            {
                errors.Add(nameof(Name), "Name is invalid");
            }

            if (!ProjectValidationHelper.IsDescriptionValid(Description))
            {
                errors.Add(nameof(Description), "Description is invalid");
            }
        }
    }
}
