using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Presenters
{
    public abstract class Presenter<T, VM> : IPresenter<T, VM> where T : ResponseModel
    {
        private ModelStateDictionary _modelState;

        private ViewDataDictionary _viewData;

        protected Presenter(ISession session, ViewDataDictionary viewData, ModelStateDictionary modelState)
        {
            _modelState = modelState;
            _viewData = viewData;

            UserId = session.GetString("UserId");
            UserName = session.GetString("UserName");
        }

        public string UserId { get; }

        public string UserName { get; }

        public VM ViewModel { get; protected set; }

        public T Response { get; private set; }

        public bool Accept(ResponseModel response)
        {
            Response = response as T;

            if (response.Message != null)
            {
                _viewData["Message"] = response.Message;
            }

            return Response != null;
        }

        public bool Accept(FailureResponse failure)
        {
            Accept(failure as ResponseModel);

            return false;
        }

        public bool Accept(ValidationErrorResponse validationError)
        {
            Accept(validationError as ResponseModel);
            //TODO: handle validation errors
            return false;
        }

        public abstract bool Accept(T success);
    }
}
