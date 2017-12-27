using System;
using System.Threading;
using ReqTrack.Domain.Core.Entities.Requirements;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectRequirement : BasicRequirement, IComparable<ProjectRequirement>
    {
        public ProjectRequirement(Identity id, RequirementType type, string title, int orderMarker) 
            : base(id, type, title)
        {
            OrderMarker = orderMarker;
        }

        public int OrderMarker { get; set; }

        public int CompareTo(ProjectRequirement other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            var typeComparison = Type.CompareTo(other.Type);

            if (typeComparison == 0)
            {
                return OrderMarker.CompareTo(other.OrderMarker);
            }

            return typeComparison;
        }
    }
}