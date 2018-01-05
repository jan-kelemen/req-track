using System.Collections.Generic;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeUseCaseOrder
{
    public class ChangeUseCaseOrderRequest : ChangeUseCaseOrderInitialRequest
    {

        public ChangeUseCaseOrderRequest(string requestedBy) : base(requestedBy)
        {
        }

        public IEnumerable<UseCase> UseCases { get; set; }
    }
}
