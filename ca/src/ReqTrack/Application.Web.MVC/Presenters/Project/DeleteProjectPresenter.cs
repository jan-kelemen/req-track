using ReqTrack.Application.Web.MVC.ViewModels.Project;
using ReqTrack.Domain.UseCases.Core.Project;

namespace ReqTrack.Application.Web.MVC.Presenters.Project
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
