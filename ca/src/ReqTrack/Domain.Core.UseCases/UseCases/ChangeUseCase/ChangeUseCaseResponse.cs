using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase
{
    public class ChangeUseCaseResponse : ResponseModel
    {
        public string ProjectId { get; set; }

        public string UseCaseId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public IEnumerable<string> Steps { get; set; }
    }
}
