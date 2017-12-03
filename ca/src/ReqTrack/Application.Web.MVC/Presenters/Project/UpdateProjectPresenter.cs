using ReqTrack.Application.Web.MVC.ViewModels.Project;
using ReqTrack.Domain.UseCases.Core.Project;

namespace ReqTrack.Application.Web.MVC.Presenters.Project
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
