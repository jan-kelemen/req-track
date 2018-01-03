using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Projects.DeleteProject
{
    public class DeleteProjectResponse : ResponseModel
    {
        internal DeleteProjectResponse(ExecutionStatus status) : base(status)
        {
        }

        public string ProjectId { get; set; }
    }
}
