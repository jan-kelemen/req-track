using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation
{
    public class ChangeInformationResponse : ResponseModel
    {
        internal ChangeInformationResponse() : base(ExecutionStatus.Success)
        {
        }

        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

