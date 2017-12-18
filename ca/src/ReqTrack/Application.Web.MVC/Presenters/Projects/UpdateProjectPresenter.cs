using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class UpdateProjectPresenter : Presenter<UpdateProjectResponse, UpdateProjectViewModel>
    {
        protected override UpdateProjectViewModel CreateViewModel(UpdateProjectResponse response)
        {
            return new UpdateProjectViewModel
            {
                Id = response.ProjectInfo.Id,
                Name = response.ProjectInfo.Name,
            };
        }
    }
}
