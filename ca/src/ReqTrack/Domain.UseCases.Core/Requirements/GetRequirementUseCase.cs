using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Requirements
{
    public class GetRequirementRequest
    {
        /// <summary>
        /// <see cref="Entity{T}.Id"/>
        /// </summary>
        public string Id { get; set; }
    }

    public class GetRequirementResponse
    {
        /// <summary>
        /// Read requirement.
        /// </summary>
        public Requirement Requirement { get; set; }
    }

    public class GetRequirementUseCase : IUseCaseInputBoundary<GetRequirementRequest, GetRequirementResponse>
    {
        private IRequirementRepository _requirementRepository;

        public GetRequirementUseCase(IRequirementRepository requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }

        public void Execute(IUseCaseOutputBoundary<GetRequirementResponse> outputBoundary, GetRequirementRequest requestModel)
        {
            var result = _requirementRepository.ReadRequirement(requestModel.Id.ToDomainIdentity());

            if (!result)
            {
                //TODO: handle error
            }

            outputBoundary.ResponseModel = new GetRequirementResponse
            {
                Requirement = result.Read.ToBoundaryObject(),
            };
        }
    }
}
