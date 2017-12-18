using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class DeleteProjectPresenter : Presenter<DeleteProjectResponse, ProjectViewModel>
    {
        protected override ProjectViewModel CreateViewModel(DeleteProjectResponse response)
        {
            return new ProjectViewModel
            {
                Id = response.Id,
                Name = "TODO",
            };
        }
    }
}
