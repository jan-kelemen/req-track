using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.CreateProject
{
    public class CreateProjectResponse : ResponseModel
    {
        public string GivenId { get; set; }
    }
}
