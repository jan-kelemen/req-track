using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Factories
{
    public interface IPresenterFactory
    {
        IPresenter<T> Default<T>(ISession s, ITempDataDictionary t, ModelStateDictionary m) where T : ResponseModel;

        IPresenter<T, VM> Default<T, VM>(ISession s, ITempDataDictionary t, ModelStateDictionary m) where T : ResponseModel;
    }
}
