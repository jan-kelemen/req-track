namespace ReqTrack.Domain.Core.UseCases.Boundary.Interfaces
{
    public interface IUseCase<in TRequest, out TResponse>
        where TRequest : RequestModel 
        where TResponse : ResponseModel
    {
        void Execute(IUseCaseOutput<TResponse> output, TRequest request);
    }
}
