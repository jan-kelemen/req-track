using ReqTrack.Runtime.Core.Registry;

namespace ReqTrack.Runtime.Core.Initialization
{
    public class Initializer
    {
        public Initializer()
        {
            var persistenceInitializer = new Persistence.Concrete.MongoDB.Initialization.Initializer();

            var securityGateway = persistenceInitializer.SecurityGatewayFactory.SecurityGateway;
            var repositoryFactory = persistenceInitializer.RepositoryFactory;

            var useCaseInitializer = new Domain.Core.UseCases.Initialization.Initializer(securityGateway, repositoryFactory);

            Registry.RegisterFactory(useCaseInitializer.ProjectUseCaseFactory);
            Registry.RegisterFactory(useCaseInitializer.RequirementUseCaseFactory);
            Registry.RegisterFactory(useCaseInitializer.UseCaseUseCaseFactory);
            Registry.RegisterFactory(useCaseInitializer.UserUseCaseFactory);
        }

        public IRegistry Registry { get; } = new RegistryData();
    }
}
