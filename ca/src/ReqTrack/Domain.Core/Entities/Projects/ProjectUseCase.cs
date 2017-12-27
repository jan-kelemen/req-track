using System;
using ReqTrack.Domain.Core.Entities.UseCases;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectUseCase : BasicUseCase, IComparable<ProjectUseCase>
    {
        public ProjectUseCase(Identity id, string title, int orderMarker) : base(id, title)
        {
            OrderMarker = orderMarker;
        }

        public int OrderMarker { get; set; }

        public int CompareTo(ProjectUseCase other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return OrderMarker.CompareTo(other.OrderMarker);
        }
    }
}
