using ReqTrack.Domain.Core.Entities.UseCases;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectUseCase : BasicUseCase
    {
        public ProjectUseCase(Identity id, string title, int orderMarker) : base(id, title)
        {
            OrderMarker = orderMarker;
        }

        public int OrderMarker { get; set; }
    }
}
