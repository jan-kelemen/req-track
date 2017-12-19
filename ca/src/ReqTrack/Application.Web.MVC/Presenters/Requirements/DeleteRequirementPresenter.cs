using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Requirements
{
    public class DeleteRequirementPresenter : Presenter<DeleteRequirementResponse, RequirementViewModel>
    {
        protected override RequirementViewModel CreateViewModel(DeleteRequirementResponse response) => new RequirementViewModel
        {
            Id = response.Id
        };
    }
}
