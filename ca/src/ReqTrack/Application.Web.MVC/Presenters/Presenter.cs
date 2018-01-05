using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Presenters
{
    public abstract class Presenter<TResponse, VM> : IPresenter<TResponse, VM> where TResponse : ResponseModel
    {
        public VM ViewModel { get; protected set; }

        public TResponse Response { get; protected set; }

        public bool Accept(FailureResponse failure)
        {
            return false;
        }

        public bool Accept(ValidationErrorResponse validationError)
        {
            return false;
        }

        public abstract bool Accept(TResponse success);
    }
}
