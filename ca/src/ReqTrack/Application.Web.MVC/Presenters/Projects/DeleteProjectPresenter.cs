using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class DeleteProjectPresenter : Presenter<DeleteProjectResponse, DeleteProjectViewModel>
    {
        protected override DeleteProjectViewModel CreateViewModel(DeleteProjectResponse response)
        {
            return new DeleteProjectViewModel
            {
                Id = response.Id,
            };
        }
    }
}
