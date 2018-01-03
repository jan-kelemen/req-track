using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.CreateProject
{
    public class CreateProjectResponse : ResponseModel
    {
        internal CreateProjectResponse(ExecutionStatus status) : base(status)
        {
        }

        public string GivenId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
