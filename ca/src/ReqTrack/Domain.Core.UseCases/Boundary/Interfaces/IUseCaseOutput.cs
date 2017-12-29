namespace ReqTrack.Domain.Core.UseCases.Boundary.Interfaces
{
    public interface IUseCaseOutput<in TResponse> where TResponse : ResponseModel
    {
        TResponse Response { set; }
    }
}
