using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder
{
    public class ChangeRequirementOrderInitialRequest : RequestModel
    {
        public ChangeRequirementOrderInitialRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string ProjectId { get; set; }

        public string Type { get; set; }

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                errors.Add(nameof(ProjectId), "Project identifier is invalid.");
            }

            if (string.IsNullOrWhiteSpace(Type))
            {
                errors.Add(nameof(Type), "Requirement type is invalid.");
            }

            return !errors.Any();
        }
    }
}
