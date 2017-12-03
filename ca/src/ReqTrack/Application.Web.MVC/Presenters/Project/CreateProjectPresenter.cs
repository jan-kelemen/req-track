using ReqTrack.Application.Web.MVC.ViewModels.Project;
using ReqTrack.Domain.UseCases.Core.Project;

namespace ReqTrack.Application.Web.MVC.Presenters.Project
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
