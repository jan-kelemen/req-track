using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Domain.UseCases.Core.Projects.ResponseModels
{
    public class GetProjectRequirementsResponse
    {
        public ProjectWithRequirements Project { get; set; }
    }
}
