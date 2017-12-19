using Microsoft.WindowsAzure.Storage.RetryPolicies;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;
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

        public IPresenter<CreateRequirementResponse, RequirementViewModel> CreateRequirement()
        {
            return new CreateRequirementPresenter(); ;
        }

        public IPresenter<DeleteRequirementResponse, RequirementViewModel> DeleteRequirement()
        {
            return new DeleteRequirementPresenter(); ;
        }

        public IPresenter<GetRequirementResponse, RequirementViewModel> GetRequirement()
        {
            return new GetRequirementPresenter(); ;
        }

        public IPresenter<GetRequirementResponse, RequirementViewModel> UpdateRequirementInitial()
        {
            return new UpdateRequirementPresenter();
        }

        public IPresenter<UpdateRequirementResponse, RequirementViewModel> UpdateRequirement()
        {
            return new UpdateRequirementPresenter();
        }
    }
}
