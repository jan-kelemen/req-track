using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Domain.UseCases.Core.Projects.ResponseModels
{
    public class UpdateProjectResponse
    {
        /// <summary>
        /// Updated project.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }
}
