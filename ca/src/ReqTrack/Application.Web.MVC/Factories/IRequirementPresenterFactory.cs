using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement;

namespace ReqTrack.Application.Web.MVC.Factories
{
    public interface IRequirementPresenterFactory : IPresenterFactory
    {
        IPresenter<ViewRequirementResponse, ViewRequirementViewModel> ViewRequirement(ISession s, ITempDataDictionary t, ModelStateDictionary m);

        IPresenter<AddRequirementResponse, AddRequirementViewModel> AddRequirement(ISession s, ITempDataDictionary t, ModelStateDictionary m);

        IPresenter<ChangeRequirementResponse, ChangeRequirementViewModel> ChangeRequirement(ISession s, ITempDataDictionary t, ModelStateDictionary m);
    }
}
