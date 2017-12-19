using ReqTrack.Application.Web.MVC.ViewModels.Extensions;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class GetProjectRequirementsPresenter : Presenter<GetProjectRequirementsResponse, ProjectWithRequirementsViewModel>
    {
        protected override ProjectWithRequirementsViewModel CreateViewModel(GetProjectRequirementsResponse response)
        {
            return response.Project.ToViewModel();
        }
    }
}
