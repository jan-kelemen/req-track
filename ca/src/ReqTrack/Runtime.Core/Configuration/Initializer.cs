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

            IProjectUseCaseFactory projectUseCaseFactory = new DefaultUseCaseFactory(repositoryFactory);
            RegistryProxy.Get.RegisterFactory(projectUseCaseFactory);
        }
    }
}
