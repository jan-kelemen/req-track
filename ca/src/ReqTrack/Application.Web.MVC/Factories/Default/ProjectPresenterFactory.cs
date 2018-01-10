using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.Presenters.Projects;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRights;
using ReqTrack.Domain.Core.UseCases.Projects.ViewProject;

namespace ReqTrack.Application.Web.MVC.Factories.Default
{
    public class ProjectPresenterFactory : PresenterFactory, IProjectPresenterFactory
    {
        public IPresenter<ViewProjectResponse, ViewProjectViewModel> ViewProject(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ViewProjectPresenter(s, t, m);
        }

        public IPresenter<ChangeInformationResponse, ChangeInformationViewModel> ChangeInformation(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ChangeInformationPresenter(s, t, m);
        }

        public IPresenter<ChangeRightsResponse, ChangeRightsViewModel> ChangeRights(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ChangeRightsPresenter(s, t, m);
        }

        public IPresenter<ChangeRequirementOrderResponse, ChangeRequirementOrderViewModel> ChangeRequirementOrder(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ChangeRequirementOrderPresenter(s, t, m);
        }
    }
}
