using ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class CreateProjectPresenter : Presenter<CreateProjectResponse, ProjectViewModel>
    {
        protected override ProjectViewModel CreateViewModel(CreateProjectResponse response)
        {
            return response.ProjectInfo.ToViewModel();
        }
    }
}
