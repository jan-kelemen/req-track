using ReqTrack.Domain.Core.UseCases.Boundary.Requests;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Boundary.Interfaces
{
    public interface IUseCase<in TRequest, out TResponse>
        where TRequest : RequestModel 
        where TResponse : ResponseModel
    {
        bool Execute(IUseCaseOutput<TResponse> output, TRequest request);
    }

    public interface IUseCase<in TInitialRequest, in TRequest, out TResponse>
        where TInitialRequest : RequestModel
        where TRequest : RequestModel
        where TResponse : ResponseModel
    {
        bool Execute(IUseCaseOutput<TResponse> output, TInitialRequest request);

        bool Execute(IUseCaseOutput<TResponse> output, TRequest request);
    }
}
