using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Project;
using ReqTrack.Domain.Core.Repositories.Factories;

namespace ReqTrack.Domain.UseCases.Core.Factories
{
    public class DefaultUseCaseFactory : IProjectUseCaseFactory
    {
        private IRepositoryFactory _repositoryFactory;

        public DefaultUseCaseFactory(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

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
    }
}
