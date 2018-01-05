using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.DeleteRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement;
namespace ReqTrack.Domain.Core.UseCases.Factories.Default
{
    internal class RequirementUseCaseFactory : IRequirementUseCaseFactory
    {
        public RequirementUseCaseFactory(ISecurityGateway securityGateway, IRepositoryFactory repositoryFactory)
        {
            AddRequirement = new AddRequirementUseCase(securityGateway, repositoryFactory.ProjectRepository, repositoryFactory.RequirementRepository, repositoryFactory.UserRepository);
            ChangeRequirement = new ChangeRequirementUseCase(securityGateway, repositoryFactory.RequirementRepository);
            DeleteRequirement = new DeleteRequirementUseCase(securityGateway, repositoryFactory.RequirementRepository);
            ViewRequirement = new ViewRequirementUseCase(securityGateway, repositoryFactory.RequirementRepository);
        }

        public IUseCase<AddRequirementRequest, AddRequirementResponse> AddRequirement { get; }
        public IUseCase<ChangeRequirementInitialRequest, ChangeRequirementRequest, ChangeRequirementResponse> ChangeRequirement { get; }
        public IUseCase<DeleteRequirementRequest, DeleteRequirementResponse> DeleteRequirement { get; }
        public IUseCase<ViewRequirementRequest, ViewRequirementResponse> ViewRequirement { get; set; }
    }
}
