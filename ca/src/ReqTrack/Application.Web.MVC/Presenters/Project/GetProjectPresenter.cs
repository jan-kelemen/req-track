using ReqTrack.Application.Web.MVC.ViewModels.Project;
using ReqTrack.Domain.UseCases.Core.Project;

namespace ReqTrack.Application.Web.MVC.Presenters.Project
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
