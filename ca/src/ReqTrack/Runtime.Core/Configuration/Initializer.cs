using ReqTrack.Domain.Core.Repositories.Factories;
using ReqTrack.Domain.UseCases.Core.Factories;
using ReqTrack.Runtime.Core.Registry;

namespace ReqTrack.Runtime.Core.Configuration
{
    public class Initializer
    {
        public void Initialize()
        {
            RegistryProxy.Get = new RegistryData();

            IRepositoryFactory repositoryFactory = new Persistence.Concrete.MongoDB.Configuration.Initializer().Initialize();
            RegistryProxy.Get.RegisterFactory(repositoryFactory);

            var factory = new DefaultUseCaseFactory(repositoryFactory);
            RegistryProxy.Get.RegisterFactory<IProjectUseCaseFactory>(factory);
            RegistryProxy.Get.RegisterFactory<IRequirementUseCaseFactory>(factory);
        }
    }
}
