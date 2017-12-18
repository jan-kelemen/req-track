using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Domain.Core.Repositories.Factories;
using ReqTrack.Domain.UseCases.Core.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Factories
{
    public class DefaultUseCaseFactory
        : IProjectUseCaseFactory
        , IRequirementUseCaseFactory
    {
        private IRepositoryFactory _repositoryFactory;

        public DefaultUseCaseFactory(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        #region IProjectUseCaseFactory implementation
        IUseCaseInputBoundary<CreateProjectRequest, CreateProjectResponse> IProjectUseCaseFactory.CreateProject()
        {
            return new CreateProjectUseCase(_repositoryFactory.ProjectRepository);
        }

        IUseCaseInputBoundary<DeleteProjectRequest, DeleteProjectResponse> IProjectUseCaseFactory.DeleteProject()
        {
            return new DeleteProjectUseCase(_repositoryFactory.ProjectRepository);
        }

        IUseCaseInputBoundary<GetAllProjectsRequest, GetAllProjectsResponse> IProjectUseCaseFactory.GetAllProjects()
        {
            return new GetAllProjectsUseCase(_repositoryFactory.ProjectRepository);
        }

        IUseCaseInputBoundary<GetProjectRequest, GetProjectResponse> IProjectUseCaseFactory.GetProject()
        {
            return new GetProjectUseCase(_repositoryFactory.ProjectRepository);
        }

        IUseCaseInputBoundary<UpdateProjectRequest, UpdateProjectResponse> IProjectUseCaseFactory.UpdateProject()
        {
            return new UpdateProjectUseCase(_repositoryFactory.ProjectRepository);
        }
        #endregion

        #region IRequirementUseCaseFactory implementation
        IUseCaseInputBoundary<CreateRequirementRequest, CreateRequirementResponse> IRequirementUseCaseFactory.CreateRequirement()
        {
            return new CreateRequirementUseCase(_repositoryFactory.RequirementRepository);
        }

        IUseCaseInputBoundary<DeleteRequirementRequest, DeleteRequirementResponse> IRequirementUseCaseFactory.DeleteRequirement()
        {
            return new DeleteRequirementUseCase(_repositoryFactory.RequirementRepository);
        }

        IUseCaseInputBoundary<GetRequirementRequest, GetRequirementResponse> IRequirementUseCaseFactory.GetRequirement()
        {
            return new GetRequirementUseCase(_repositoryFactory.RequirementRepository);
        }

        IUseCaseInputBoundary<UpdateRequirementRequest, UpdateRequirementResponse> IRequirementUseCaseFactory.UpdateRequirement()
        {
            return new UpdateRequirementUseCase(_repositoryFactory.RequirementRepository);
        }
        #endregion
    }
}
