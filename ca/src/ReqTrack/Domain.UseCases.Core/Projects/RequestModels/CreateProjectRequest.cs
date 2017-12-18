using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Domain.UseCases.Core.Projects.RequestModels
{
    public class CreateProjectRequest
    {
        /// <summary>
        /// Project to be created. Identifier field is ignored.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }
}
