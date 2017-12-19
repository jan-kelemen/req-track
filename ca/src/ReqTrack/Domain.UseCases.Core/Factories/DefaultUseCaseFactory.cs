using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Domain.Core.Repositories.Factories;
using ReqTrack.Domain.UseCases.Core.Requirements;
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
        ICreateProjectUseCase IProjectUseCaseFactory.CreateProject() => new CreateProjectUseCase(_repositoryFactory.ProjectRepository);

        IDeleteProjectUseCase IProjectUseCaseFactory.DeleteProject() => new DeleteProjectUseCase(_repositoryFactory.ProjectRepository);

        IGetAllProjectsUseCase IProjectUseCaseFactory.GetAllProjects() => new GetAllProjectsUseCase(_repositoryFactory.ProjectRepository);

        IGetProjectUseCase IProjectUseCaseFactory.GetProject() => new GetProjectUseCase(_repositoryFactory.ProjectRepository);

        IUpdateProjectUseCase IProjectUseCaseFactory.UpdateProject() => new UpdateProjectUseCase(_repositoryFactory.ProjectRepository);

        IGetProjectRequirementsUseCase IProjectUseCaseFactory.GetProjectRequirements() => new GetProjectRequirementsUseCase(_repositoryFactory.ProjectRepository);
        #endregion

        #region IRequirementUseCaseFactory implementation
        ICreateRequirementUseCase IRequirementUseCaseFactory.CreateRequirement() => new CreateRequirementUseCase(_repositoryFactory.RequirementRepository);

        IDeleteRequirementUseCase IRequirementUseCaseFactory.DeleteRequirement() => new DeleteRequirementUseCase(_repositoryFactory.RequirementRepository);

        IGetRequirementUseCase IRequirementUseCaseFactory.GetRequirement() => new GetRequirementUseCase(_repositoryFactory.RequirementRepository);

        IUpdateRequirementUseCase IRequirementUseCaseFactory.UpdateRequirement() => new UpdateRequirementUseCase(_repositoryFactory.RequirementRepository);
        #endregion
    }
}
