using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase
{
    public class AddUseCaseResponse : ResponseModel
    {
        public AddUseCaseResponse() : base(ExecutionStatus.Success)
        {
        }

        public string GivenId { get; set; }
    }
}
