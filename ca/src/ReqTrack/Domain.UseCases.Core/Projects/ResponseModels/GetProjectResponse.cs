using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Domain.UseCases.Core.Projects.ResponseModels
{
    public class GetProjectResponse
    {
        /// <summary>
        /// Read project.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }
}
