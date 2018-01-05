using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation
{
    public class ChangeInformationResponse : ResponseModel
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

