using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Boundary.Interfaces
{
    public interface IUseCaseOutput<in TSuccess> where TSuccess : ResponseModel
    {
        bool Accept(FailureResponse failure);

        bool Accept(ValidationErrorResponse validationError);

        bool Accept(TSuccess success);
    }
}
