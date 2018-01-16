using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.DeleteUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase;

namespace ReqTrack.Domain.Core.UseCases.Factories.Default
{
    internal class UseCaseUseCaseFactory : IUseCaseUseCaseFactory
    {
        public UseCaseUseCaseFactory(ISecurityGateway securityGateway, IRepositoryFactory repositoryFactory)
        {
            AddUseCase = new AddUseCaseUseCase(securityGateway, repositoryFactory.ProjectRepository, repositoryFactory.UseCaseRepository, repositoryFactory.UserRepository);
            ChangeUseCase = new ChangeUseCaseUseCase(securityGateway, repositoryFactory.UseCaseRepository);
            DeleteUseCase = new DeleteUseCaseUseCase(securityGateway, repositoryFactory.UseCaseRepository);
            ViewUseCase = new ViewUseCaseUseCase(securityGateway, repositoryFactory.UseCaseRepository);
        }

        public IUseCase<AddUseCaseInitialRequest, AddUseCaseRequest, AddUseCaseResponse> AddUseCase { get; }
        public IUseCase<ChangeUseCaseInitialRequest, ChangeUseCaseRequest, ChangeUseCaseResponse> ChangeUseCase { get; }
        public IUseCase<DeleteUseCaseRequest, DeleteUseCaseResponse> DeleteUseCase { get; }
        public IUseCase<ViewUseCaseRequest, ViewUseCaseResponse> ViewUseCase { get; set; }
    }
}
