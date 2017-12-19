using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Application.Web.MVC.Presenters.Projects;
using ReqTrack.Application.Web.MVC.Presenters.Requirements;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public class DefaultPresenterFactory
        : IProjectPresenterFactory
        , IRequirementPresenterFactory
    {
        public IPresenter<CreateProjectResponse, ProjectViewModel> CreateProject() => new CreateProjectPresenter();

        public IPresenter<DeleteProjectResponse, ProjectViewModel> DeleteProject() => new DeleteProjectPresenter();

        public IPresenter<GetAllProjectsResponse, ProjectsViewModel> GetAllProjects() => new GetAllProjectsPresenter();

        public IPresenter<GetProjectResponse, ProjectViewModel> GetProject() => new GetProjectPresenter();

        public IPresenter<UpdateProjectResponse, ProjectViewModel> UpdateProject() => new UpdateProjectPresenter();

        public IPresenter<GetProjectResponse, ProjectViewModel> UpdateProjectInitial() => new UpdateProjectPresenter();

        public IPresenter<CreateRequirementResponse, RequirementViewModel> CreateRequirement() => new CreateRequirementPresenter();

        public IPresenter<DeleteRequirementResponse, RequirementViewModel> DeleteRequirement() => new DeleteRequirementPresenter();

        public IPresenter<GetRequirementResponse, RequirementViewModel> GetRequirement() => new GetRequirementPresenter();

        public IPresenter<GetRequirementResponse, RequirementViewModel> UpdateRequirementInitial() => new UpdateRequirementPresenter();

        public IPresenter<UpdateRequirementResponse, RequirementViewModel> UpdateRequirement() => new UpdateRequirementPresenter();
    }
}
