using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using System.Linq;
using ReqTrack.Application.Web.MVC.ViewModels.Extensions;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class GetAllProjectsPresenter : Presenter<GetAllProjectsResponse, ProjectsViewModel>
    {
        protected override ProjectsViewModel CreateViewModel(GetAllProjectsResponse response)
        {
            return new ProjectsViewModel
            {
                Projects = response.Projects.Select(p => p.ToViewModel()),
            };
        }
    }
}
