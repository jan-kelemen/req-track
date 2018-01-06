using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Presenters
{
    public interface IPresenter<T> : IUseCaseOutput<T> where T : ResponseModel
    {
        T Response { get; }
    }

    public interface IPresenter<T, out VM> : IPresenter<T>  where T : ResponseModel
    {
        VM ViewModel { get; }
    }
}
