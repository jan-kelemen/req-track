namespace ReqTrack.Domain.Core.UseCases.Boundary.Interfaces
{
    public interface IUseCase<in TRequest, out TResponse>
        where TRequest : RequestModel 
        where TResponse : ResponseModel
    {
        void Execute(IUseCaseOutput<TResponse> output, TRequest request);
    }

    public interface IUseCase<in TInitialRequest, in TRequest, out TResponse>
        where TInitialRequest : RequestModel
        where TRequest : RequestModel
        where TResponse : ResponseModel
    {
        void Execute(IUseCaseOutput<TResponse> output, TInitialRequest request);

        void Execute(IUseCaseOutput<TResponse> output, TRequest request);
    }
}
