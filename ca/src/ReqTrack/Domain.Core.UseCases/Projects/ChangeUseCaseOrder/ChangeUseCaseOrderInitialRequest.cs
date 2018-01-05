using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeUseCaseOrder
{
    public class ChangeUseCaseOrderInitialRequest : RequestModel
    {
        public ChangeUseCaseOrderInitialRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string ProjectId { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                errors.Add(nameof(ProjectId), "Project identifier is invalid.");
            }
        }
    }
}
