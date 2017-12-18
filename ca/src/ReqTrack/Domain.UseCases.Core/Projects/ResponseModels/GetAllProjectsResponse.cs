using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using System.Collections.Generic;

namespace ReqTrack.Domain.UseCases.Core.Projects.ResponseModels
{
    public class GetAllProjectsResponse
    {
        /// <summary>
        /// List of all projects.
        /// </summary>
        public IEnumerable<ProjectInfo> Projects { get; set; }
    }
}
