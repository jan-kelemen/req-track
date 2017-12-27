using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IUseCaseRepository : IRepository
    {
        CreateResult<UseCase> CreateUseCase(UseCase useCase);

        ReadResult<UseCase> ReadUseCase(Identity id);

        UpdateResult<UseCase> UpdateUseCase(UseCase useCase);

        DeleteResult<Identity> DeleteUseCase(Identity id);
    }
}
