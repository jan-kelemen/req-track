using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Application.Web.MVC.Presenters.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public class DefaultPresenterFactory : IProjectPresenterFactory
    {
        public IPresenter<CreateProjectResponse, ProjectInfoViewModel> CreateProject()
        {
            return new CreateProjectPresenter();
        }

        public IPresenter<DeleteProjectResponse, ProjectInfoViewModel> DeleteProject()
        {
            return new DeleteProjectPresenter();
        }

        public IPresenter<GetAllProjectsResponse, ProjectInfosViewModel> GetAllProjects()
        {
            return new GetAllProjectsPresenter();
        }

        public IPresenter<GetProjectResponse, ProjectInfoViewModel> GetProject()
        {
            return new GetProjectPresenter();
        }

        public IPresenter<UpdateProjectResponse, ProjectInfoViewModel> UpdateProject()
        {
            return new UpdateProjectPresenter();
        }

        public IPresenter<GetProjectResponse, ProjectInfoViewModel> UpdateProjectInitial()
        {
            return new UpdateProjectPresenter();
        }
    }
}
