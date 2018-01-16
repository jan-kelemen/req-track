using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Factories;
using ReqTrack.Domain.Core.UseCases.Factories.Default;

namespace ReqTrack.Domain.Core.UseCases.Initialization
{
    public class Initializer
    {
        public Initializer(ISecurityGateway securityGateway, IRepositoryFactory repositoryFactory)
        {
            UserUseCaseFactory = new UserUseCaseFactory(securityGateway, repositoryFactory);
            ProjectUseCaseFactory = new ProjectUseCaseFactory(securityGateway, repositoryFactory);
            RequirementUseCaseFactory = new RequirementUseCaseFactory(securityGateway, repositoryFactory);
            UseCaseUseCaseFactory = new UseCaseUseCaseFactory(securityGateway, repositoryFactory);
        }

        public IUserUseCaseFactory UserUseCaseFactory { get; }

        public IProjectUseCaseFactory ProjectUseCaseFactory { get; }

        public IRequirementUseCaseFactory RequirementUseCaseFactory { get; }

        public IUseCaseUseCaseFactory UseCaseUseCaseFactory { get; }
    }
}
