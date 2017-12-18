using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Application.Web.MVC.Presenters.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public class DefaultPresenterFactory : IProjectPresenterFactory
    {
        public IPresenter<CreateProjectResponse, ProjectViewModel> CreateProject()
        {
            return new CreateProjectPresenter();
        }

        public IPresenter<DeleteProjectResponse, ProjectViewModel> DeleteProject()
        {
            return new DeleteProjectPresenter();
        }

        public IPresenter<GetAllProjectsResponse, ProjectsViewModel> GetAllProjects()
        {
            return new GetAllProjectsPresenter();
        }

        public IPresenter<GetProjectResponse, ProjectViewModel> GetProject()
        {
            return new GetProjectPresenter();
        }

        public IPresenter<UpdateProjectResponse, ProjectViewModel> UpdateProject()
        {
            return new UpdateProjectPresenter();
        }

        public IPresenter<GetProjectResponse, ProjectViewModel> UpdateProjectInitial()
        {
            return new UpdateProjectPresenter();
        }
    }
}
