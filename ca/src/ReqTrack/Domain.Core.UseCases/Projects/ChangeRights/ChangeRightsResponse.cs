using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRights
{
    public class ChangeRightsResponse : ResponseModel
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProjectRights> Rights { get; set; }
    }
}
