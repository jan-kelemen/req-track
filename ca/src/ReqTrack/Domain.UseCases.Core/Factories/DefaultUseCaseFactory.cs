using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Domain.Core.Repositories.Factories;
using ReqTrack.Domain.UseCases.Core.Requirements;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;
using ReqTrack.Domain.UseCases.Core.Requirements.RequestModels;
using ReqTrack.Domain.UseCases.Core.Projects.Interfaces;
using ReqTrack.Domain.UseCases.Core.Requirements.Interfaces;

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
        ICreateProjectUseCase IProjectUseCaseFactory.CreateProject()
        {
            return new CreateProjectUseCase(_repositoryFactory.ProjectRepository);
        }

        IDeleteProjectUseCase IProjectUseCaseFactory.DeleteProject()
        {
            return new DeleteProjectUseCase(_repositoryFactory.ProjectRepository);
        }

        IGetAllProjectsUseCase IProjectUseCaseFactory.GetAllProjects()
        {
            return new GetAllProjectsUseCase(_repositoryFactory.ProjectRepository);
        }

        IGetProjectUseCase IProjectUseCaseFactory.GetProject()
        {
            return new GetProjectUseCase(_repositoryFactory.ProjectRepository);
        }

        IUpdateProjectUseCase IProjectUseCaseFactory.UpdateProject()
        {
            return new UpdateProjectUseCase(_repositoryFactory.ProjectRepository);
        }
        #endregion

        #region IRequirementUseCaseFactory implementation
        ICreateRequirementUseCase IRequirementUseCaseFactory.CreateRequirement()
        {
            return new CreateRequirementUseCase(_repositoryFactory.RequirementRepository);
        }

        IDeleteRequirementUseCase IRequirementUseCaseFactory.DeleteRequirement()
        {
            return new DeleteRequirementUseCase(_repositoryFactory.RequirementRepository);
        }

        IGetRequirementUseCase IRequirementUseCaseFactory.GetRequirement()
        {
            return new GetRequirementUseCase(_repositoryFactory.RequirementRepository);
        }

        IUpdateRequirementUseCase IRequirementUseCaseFactory.UpdateRequirement()
        {
            return new UpdateRequirementUseCase(_repositoryFactory.RequirementRepository);
        }
        #endregion
    }
}
