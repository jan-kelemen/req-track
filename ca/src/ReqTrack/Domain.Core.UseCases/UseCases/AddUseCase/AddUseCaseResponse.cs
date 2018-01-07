using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase
{
    public class AddUseCaseResponse : ResponseModel
    {
        public string GivenId { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
