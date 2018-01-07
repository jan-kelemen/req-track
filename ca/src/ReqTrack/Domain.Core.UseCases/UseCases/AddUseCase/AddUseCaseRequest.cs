using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase
{
    public class AddUseCaseRequest : AddUseCaseInitialRequest
    {
        public AddUseCaseRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string Title { get; set; }

        public string Note { get; set; }

        public IEnumerable<string> Steps { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (!RequirementValidationHelper.IsTitleValid(Title))
            {
                errors.Add(nameof(Title), "Title is invalid");
            }

            if (!RequirementValidationHelper.IsNoteValid(Note))
            {
                errors.Add(nameof(Note), "Note is invalid");
            }
        }
    }
}
