using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeUseCaseOrder
{
    public class ChangeUseCaseOrderResponse : ResponseModel
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public IEnumerable<UseCase> UseCases { get; set; }
    }
}
