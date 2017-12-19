using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public interface IRequirementPresenterFactory
    {
        IPresenter<CreateRequirementResponse, RequirementViewModel> CreateRequirement();

        IPresenter<DeleteRequirementResponse, RequirementViewModel> DeleteRequirement();

        IPresenter<GetRequirementResponse, RequirementViewModel> GetRequirement();

        IPresenter<GetRequirementResponse, RequirementViewModel> UpdateRequirementInitial();

        IPresenter<UpdateRequirementResponse, RequirementViewModel> UpdateRequirement();
    }
}
