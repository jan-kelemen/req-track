using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class DeleteProjectPresenter : Presenter<DeleteProjectResponse, ProjectInfoViewModel>
    {
        protected override ProjectInfoViewModel CreateViewModel(DeleteProjectResponse response)
        {
            return new ProjectInfoViewModel
            {
                Id = response.Id,
                Name = "TODO",
            };
        }
    }
}
