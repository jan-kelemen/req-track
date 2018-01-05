using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase
{
    public class ViewUseCaseResponse : ResponseModel
    {
        public string UseCaseId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public IEnumerable<string> Steps { get; set; }

        public Project Project { get; set; }

        public User Author { get; set; }
    }
}
