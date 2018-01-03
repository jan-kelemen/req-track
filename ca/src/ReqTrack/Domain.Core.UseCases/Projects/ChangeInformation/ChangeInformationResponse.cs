using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation
{
    public class ChangeInformationResponse : ResponseModel
    {
        internal ChangeInformationResponse(ExecutionStatus status) : base(status)
        {
        }

        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

