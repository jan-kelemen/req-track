using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.Presenters.UseCases;
using ReqTrack.Application.Web.MVC.ViewModels.UseCases;
using ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase;

namespace ReqTrack.Application.Web.MVC.Factories.Default
{
    public class UseCasePresenterFactory : PresenterFactory, IUseCasePresenterFactory
    {
        public IPresenter<ViewUseCaseResponse, ViewUseCaseViewModel> ViewUseCase(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ViewUseCasePresenter(s, t, m);
        }

        public IPresenter<ChangeUseCaseResponse, ChangeUseCaseViewModel> ChangeUseCase(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ChangeUseCasePresenter(s, t, m);
        }

        public IPresenter<AddUseCaseResponse, AddUseCaseViewModel> AddUseCase(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new AddUseCasePresenter(s, t, m);
        }
    }
}
