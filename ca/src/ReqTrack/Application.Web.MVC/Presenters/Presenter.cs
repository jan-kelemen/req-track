using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Presenters
{
    public class Presenter<T> : IPresenter<T> where T : ResponseModel
    {
        private ModelStateDictionary _modelState;

        private ITempDataDictionary _tempData;

        public Presenter(ISession session, ITempDataDictionary tempData, ModelStateDictionary modelState)
        {
            _modelState = modelState;
            _tempData = tempData;

            UserId = session.GetString("UserId");
            UserName = session.GetString("UserName");
        }

        public string UserId { get; }

        public string UserName { get; }

        public T Response { get; private set; }

        public bool Accept(ResponseModel response)
        {
            Response = response as T;

            if (response.Message != null)
            {
                _tempData["Message"] = response.Message;
            }

            return Response != null;
        }

        public bool Accept(FailureResponse failure) => Accept(failure as ResponseModel);

        public bool Accept(ValidationErrorResponse validationError)
        {
            Accept(validationError as ResponseModel);
            foreach (var error in validationError.ValidationErrors)
            {
                _modelState.AddModelError(error.Key, error.Value);
            }
            return false;
        }

        public virtual bool Accept(T success) => Accept(success as ResponseModel);
    }

    public class Presenter<T, VM> : Presenter<T>, IPresenter<T, VM> where T : ResponseModel
    {
        public Presenter(ISession session, ITempDataDictionary tempData, ModelStateDictionary modelState) 
            : base(session, tempData, modelState)
        {
        }

        public VM ViewModel { get; protected set; }
    }
}
