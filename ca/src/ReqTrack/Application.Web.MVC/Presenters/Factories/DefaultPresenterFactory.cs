using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Application.Web.MVC.Presenters.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public class DefaultPresenterFactory : IProjectPresenterFactory
    {
        public IPresenter<CreateProjectResponse, CreateProjectViewModel> CreateProject()
        {
            return new CreateProjectPresenter();
        }

        public IPresenter<DeleteProjectResponse, DeleteProjectViewModel> DeleteProject()
        {
            return new DeleteProjectPresenter();
        }

        public IPresenter<GetAllProjectsResponse, GetAllProjectsViewModel> GetAllProjects()
        {
            return new GetAllProjectsPresenter();
        }

        public IPresenter<GetProjectResponse, GetProjectViewModel> GetProject()
        {
            return new GetProjectPresenter();
        }

        public IPresenter<UpdateProjectResponse, UpdateProjectViewModel> UpdateProject()
        {
            return new UpdateProjectPresenter();
        }

        public IPresenter<GetProjectResponse, UpdateProjectViewModel> UpdateProjectInitial()
        {
            return new UpdateProjectPresenter();
        }
    }
}
