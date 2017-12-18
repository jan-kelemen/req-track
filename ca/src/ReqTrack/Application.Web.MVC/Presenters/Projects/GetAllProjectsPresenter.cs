using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects;
using System.Linq;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class GetAllProjectsPresenter : Presenter<GetAllProjectsResponse, ProjectInfosViewModel>
    {
        protected override ProjectInfosViewModel CreateViewModel(GetAllProjectsResponse response)
        {
            return new ProjectInfosViewModel
            {
                Projects = response.Projects.Select(p => p.ToViewModel()),
            };
        }
    }
}
