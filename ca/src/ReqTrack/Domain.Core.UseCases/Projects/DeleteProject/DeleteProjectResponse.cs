using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.DeleteProject
{
    public class DeleteProjectResponse : ResponseModel
    {
        internal DeleteProjectResponse() : base(ExecutionStatus.Success)
        {
        }

        public string ProjectId { get; set; }
    }
}
