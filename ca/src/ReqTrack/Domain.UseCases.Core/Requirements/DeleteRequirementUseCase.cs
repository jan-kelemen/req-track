using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Requirements.Interfaces;
using ReqTrack.Domain.UseCases.Core.Requirements.RequestModels;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Requirements
{
    public class DeleteRequirementUseCase : IDeleteRequirementUseCase
    {
        private readonly IRequirementRepository _requirementRepository;

        public DeleteRequirementUseCase(IRequirementRepository requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }

        public void Execute(IUseCaseOutputBoundary<DeleteRequirementResponse> outputBoundary, DeleteRequirementRequest requestModel)
        {
            var result = _requirementRepository.DeleteRequirement(requestModel.Id.ToDomainIdentity());

            outputBoundary.ResponseModel = new DeleteRequirementResponse
            {
                Id = result.Deleted.ToBoundaryIdentity(),
            };
        }
    }
}
