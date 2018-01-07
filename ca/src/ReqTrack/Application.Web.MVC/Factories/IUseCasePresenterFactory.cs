using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.ViewModels.UseCases;
using ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase;

namespace ReqTrack.Application.Web.MVC.Factories
{
    public interface IUseCasePresenterFactory : IPresenterFactory
    {
        IPresenter<ViewUseCaseResponse, ViewUseCaseViewModel> ViewUseCase(ISession s, ITempDataDictionary t, ModelStateDictionary m);

        IPresenter<ChangeUseCaseResponse, ChangeUseCaseViewModel> ChangeUseCase(ISession s, ITempDataDictionary t, ModelStateDictionary m);

        IPresenter<AddUseCaseResponse, AddUseCaseViewModel> AddUseCase(ISession s, ITempDataDictionary t, ModelStateDictionary m);
    }
}
