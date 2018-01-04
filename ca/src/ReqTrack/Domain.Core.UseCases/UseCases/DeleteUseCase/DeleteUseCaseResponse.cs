using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.UseCases.DeleteUseCase
{
    public class DeleteUseCaseResponse : ResponseModel
    {
        internal DeleteUseCaseResponse() : base(ExecutionStatus.Success)
        {
        }
    }
}
