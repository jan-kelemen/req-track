using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;
namespace ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase
{
    public class AddUseCaseRequest : RequestModel
    {
        public AddUseCaseRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string ProjectId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public IEnumerable<string> Steps { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                errors.Add(nameof(ProjectId), "Project identifier is invalid");
            }

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
