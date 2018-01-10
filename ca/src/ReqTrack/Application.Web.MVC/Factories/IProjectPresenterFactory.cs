using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRights;
using ReqTrack.Domain.Core.UseCases.Projects.ViewProject;

namespace ReqTrack.Application.Web.MVC.Factories
{
    public interface IProjectPresenterFactory : IPresenterFactory
    {
        IPresenter<ViewProjectResponse, ViewProjectViewModel> ViewProject(ISession s, ITempDataDictionary t, ModelStateDictionary m);

        IPresenter<ChangeInformationResponse, ChangeInformationViewModel> ChangeInformation(ISession s, ITempDataDictionary t, ModelStateDictionary m);

        IPresenter<ChangeRightsResponse, ChangeRightsViewModel> ChangeRights(ISession s, ITempDataDictionary t, ModelStateDictionary m);

        IPresenter<ChangeRequirementOrderResponse, ChangeRequirementOrderViewModel> ChangeRequirementOrder(ISession s, ITempDataDictionary t, ModelStateDictionary m);
    }
}
