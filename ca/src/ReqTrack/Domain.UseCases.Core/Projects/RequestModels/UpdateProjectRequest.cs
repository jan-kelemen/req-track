using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Domain.UseCases.Core.Projects.RequestModels
{
    public class UpdateProjectRequest
    {
        /// <summary>
        /// Project to be updated.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }
}
