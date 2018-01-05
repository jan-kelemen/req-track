using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;
namespace ReqTrack.Domain.Core.UseCases.Requirements.DeleteRequirement
{
    public class DeleteRequirementRequest : RequestModel
    {
        public DeleteRequirementRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string ProjectId { get; set; }

        public string RequirementId { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                errors.Add(nameof(ProjectId), "Project identifier is invalid");
            }
        }
    }
}
