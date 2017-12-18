using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public interface IProjectPresenterFactory
    {
        IPresenter<CreateProjectResponse, ProjectInfoViewModel> CreateProject();

        IPresenter<DeleteProjectResponse, ProjectInfoViewModel> DeleteProject();

        IPresenter<GetAllProjectsResponse, ProjectInfosViewModel> GetAllProjects();

        IPresenter<GetProjectResponse, ProjectInfoViewModel> GetProject();

        IPresenter<GetProjectResponse, ProjectInfoViewModel> UpdateProjectInitial();

        IPresenter<UpdateProjectResponse, ProjectInfoViewModel> UpdateProject();
    }
}
