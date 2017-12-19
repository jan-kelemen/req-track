using ReqTrack.Application.Web.MVC.ViewModels.Extensions;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class GetProjectPresenter : Presenter<GetProjectResponse, ProjectViewModel>
    {
        protected override ProjectViewModel CreateViewModel(GetProjectResponse response) => response.ProjectInfo.ToViewModel();
    }
}
