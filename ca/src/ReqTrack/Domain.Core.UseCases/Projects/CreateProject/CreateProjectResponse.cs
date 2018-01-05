using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Projects.CreateProject
{
    public class CreateProjectResponse : ResponseModel
    {
        public string GivenId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
