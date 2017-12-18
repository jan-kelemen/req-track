using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class GetProjectPresenter : Presenter<GetProjectResponse, GetProjectViewModel>
    {
        protected override GetProjectViewModel CreateViewModel(GetProjectResponse response)
        {
            return new GetProjectViewModel
            {
                Id = response.ProjectInfo.Id,
                Name = response.ProjectInfo.Name,
            };
        }
    }
}
