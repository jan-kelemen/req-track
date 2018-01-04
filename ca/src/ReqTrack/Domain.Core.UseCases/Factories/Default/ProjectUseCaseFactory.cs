using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRights;
using ReqTrack.Domain.Core.UseCases.Projects.CreateProject;
using ReqTrack.Domain.Core.UseCases.Projects.DeleteProject;
using ReqTrack.Domain.Core.UseCases.Projects.ViewProject;

namespace ReqTrack.Domain.Core.UseCases.Factories.Default
{
    internal class ProjectUseCaseFactory : IProjectUseCaseFactory
    {
        public ProjectUseCaseFactory(ISecurityGateway securityGateway, IRepositoryFactory repositoryFactory)
        {
            CreateProject = new CreateProjectUseCase(securityGateway, repositoryFactory.ProjectRepository, repositoryFactory.UserRepository);
            ViewProject = new ViewProjectUseCase(securityGateway, repositoryFactory.ProjectRepository);
            ChangeInformation = new ChangeInformationUseCase(securityGateway, repositoryFactory.ProjectRepository);
            ChangeRights = new ChangeRightsUseCase(securityGateway, repositoryFactory.ProjectRepository);
            DeleteProject = new DeleteProjectUseCase(securityGateway, repositoryFactory.ProjectRepository);
            ChangeRequirementOrder = new ChangeRequirementOrderUseCase(securityGateway, repositoryFactory.ProjectRepository);
        }

        public IUseCase<CreateProjectRequest, CreateProjectResponse> CreateProject { get; }

        public IUseCase<ViewProjectRequest, ViewProjectResponse> ViewProject { get; }

        public IUseCase<ChangeInformationInitialRequest, ChangeInformationRequest, ChangeInformationResponse> ChangeInformation { get; }

        public IUseCase<ChangeRightsInitialRequest, ChangeRightsRequest, ChangeRightsResponse> ChangeRights { get; }

        public IUseCase<DeleteProjectRequest, DeleteProjectResponse> DeleteProject { get; }

        public IUseCase<ChangeRequirementOrderInitialRequest, ChangeRequirementOrderRequest, ChangeRequirementOrderResponse> ChangeRequirementOrder { get; }
    }
}
