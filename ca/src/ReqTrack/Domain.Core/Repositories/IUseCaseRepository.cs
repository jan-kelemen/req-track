using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.UseCases;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IUseCaseRepository : IRepository
    {
        Identity CreateUseCase(UseCase useCase);

        UseCase ReadUseCase(Identity id);

        bool UpdateUseCase(UseCase useCase);

        bool DeleteUseCase(Identity id);
    }
}
