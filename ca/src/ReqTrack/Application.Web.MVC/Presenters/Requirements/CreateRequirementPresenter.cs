using ReqTrack.Application.Web.MVC.ViewModels.Extensions;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Requirements
{
    public class CreateRequirementPresenter : Presenter<CreateRequirementResponse, RequirementViewModel>
    {
        protected override RequirementViewModel CreateViewModel(CreateRequirementResponse response) => response.Requirement.ToViewModel();
    }
}
