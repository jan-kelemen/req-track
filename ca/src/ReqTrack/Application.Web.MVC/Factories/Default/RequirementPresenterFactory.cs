using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.Presenters.Requirements;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement;

namespace ReqTrack.Application.Web.MVC.Factories.Default
{
    public class RequirementPresenterFactory : PresenterFactory, IRequirementPresenterFactory
    {
        public IPresenter<ViewRequirementResponse, ViewRequirementViewModel> ViewRequirement(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ViewRequirementPresenter(s, t, m);
        }

        public IPresenter<AddRequirementResponse, AddRequirementViewModel> AddRequirement(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new AddRequirementPresenter(s, t, m);
        }

        public IPresenter<ChangeRequirementResponse, ChangeRequirementViewModel> ChangeRequirement(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ChangeRequirementPresenter(s, t, m);
        }
    }
}
