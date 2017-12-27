using ReqTrack.Domain.Core.Repositories;

namespace ReqTrack.Domain.Core.Factories
{
    public interface IRepositoryFactory
    {
        IUserRepository UserRepository { get; }

        IProjectRepository ProjectRepository { get; }

        IRequirementRepository RequirementRepository { get; }

        IUseCaseRepository UseCaseRepository { get; }
    }
}
