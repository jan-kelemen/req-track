using ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class CreateProjectPresenter : Presenter<CreateProjectResponse, ProjectInfoViewModel>
    {
        protected override ProjectInfoViewModel CreateViewModel(CreateProjectResponse response)
        {
            return response.ProjectInfo.ToViewModel();
        }
    }
}
