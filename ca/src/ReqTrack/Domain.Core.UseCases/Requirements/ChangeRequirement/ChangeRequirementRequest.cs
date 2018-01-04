using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement
{
    public class ChangeRequirementRequest : ChangeRequirementInitialRequest
    {
        public ChangeRequirementRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (!RequirementValidationHelper.IsTitleValid(Title))
            {
                errors.Add(nameof(Title), "Title is invalid");
            }

            if (!RequirementValidationHelper.IsNoteValid(Note))
            {
                errors.Add(nameof(Note), "Note is invalid");
            }

            return !errors.Any();
        }
    }
}
