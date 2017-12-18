using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class CreateProjectPresenter : Presenter<CreateProjectResponse, CreateProjectViewModel>
    {
        protected override CreateProjectViewModel CreateViewModel(CreateProjectResponse response)
        {
            var vm = new CreateProjectViewModel
            {
                Id = response.ProjectInfo.Id,
                Name = response.ProjectInfo.Name,
            };

            return vm;
        }
    }
}
