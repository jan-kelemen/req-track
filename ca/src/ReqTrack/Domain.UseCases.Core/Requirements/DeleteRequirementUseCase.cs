using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;

namespace ReqTrack.Domain.UseCases.Core.Requirements
{
    public class DeleteRequirementRequest
    {
        /// <summary>
        /// Identifier of the requirement to be deleted, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteRequirementResponse
    {
        /// <summary>
        /// Identifier of the deleted requirement, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteRequirementUseCase : IUseCaseInputBoundary<DeleteRequirementRequest, DeleteRequirementResponse>
    {
        private IRequirementRepository _requirementRepository;

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
