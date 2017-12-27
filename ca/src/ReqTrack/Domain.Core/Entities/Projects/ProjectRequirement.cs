using ReqTrack.Domain.Core.Entities.Requirements;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectRequirement : BasicRequirement
    {
        public ProjectRequirement(Identity id, RequirementType type, string title, int orderMarker) 
            : base(id, type, title)
        {
            OrderMarker = orderMarker;
        }

        public int OrderMarker { get; set; }
    }
}