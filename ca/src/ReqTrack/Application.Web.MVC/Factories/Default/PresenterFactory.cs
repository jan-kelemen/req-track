using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Application.Web.MVC.Factories.Default
{
    public class PresenterFactory : IPresenterFactory
    {
        public IPresenter<T> Default<T>(ISession s, ITempDataDictionary t, ModelStateDictionary m) where T : ResponseModel
        {
            return new Presenter<T>(s, t, m);
        }

        public IPresenter<T, VM> Default<T, VM>(ISession s, ITempDataDictionary t, ModelStateDictionary m) where T : ResponseModel
        {
            return new Presenter<T, VM>(s, t, m);
        }
    }
}
