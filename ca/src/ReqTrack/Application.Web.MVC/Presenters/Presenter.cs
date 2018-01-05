using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Presenters
{
    public abstract class Presenter<T, VM> : IPresenter<T, VM> where T : ResponseModel
    {
        public VM ViewModel { get; protected set; }

        public T Response { get; protected set; }

        public bool Accept(FailureResponse failure)
        {
            return false;
        }

        public bool Accept(ValidationErrorResponse validationError)
        {
            return false;
        }

        public abstract bool Accept(T success);
    }
}
