using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public interface IProjectPresenterFactory
    {
        IPresenter<CreateProjectResponse, ProjectViewModel> CreateProject();

        IPresenter<DeleteProjectResponse, ProjectViewModel> DeleteProject();

        IPresenter<GetAllProjectsResponse, ProjectsViewModel> GetAllProjects();

        IPresenter<GetProjectResponse, ProjectViewModel> GetProject();

        IPresenter<GetProjectResponse, ProjectViewModel> UpdateProjectInitial();

        IPresenter<UpdateProjectResponse, ProjectViewModel> UpdateProject();
    }
}
