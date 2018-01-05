using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;
namespace ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase
{
    public class ViewUseCaseRequest : RequestModel
    {
        public ViewUseCaseRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string ProjectId { get; set; }

        public string UseCaseId { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                errors.Add(nameof(ProjectId), "Project identifier is invalid");
            }

            if (string.IsNullOrWhiteSpace(UseCaseId))
            {
                errors.Add(nameof(UseCaseId), "Use case identifier is invalid");
            }
        }
    }
}
