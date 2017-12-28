using System;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    public class MongoUseCaseRepository : MongoBaseRepository, IUseCaseRepository
    {
        public CreateResult<UseCase> CreateUseCase(UseCase useCase)
        {
            throw new NotImplementedException();
        }

        public ReadResult<UseCase> ReadUseCase(Identity id)
        {
            throw new NotImplementedException();
        }

        public UpdateResult<UseCase> UpdateUseCase(UseCase useCase)
        {
            throw new NotImplementedException();
        }

        public DeleteResult<Identity> DeleteUseCase(Identity id)
        {
            throw new NotImplementedException();
        }
    }
}
