using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Requirements.Interfaces;
using ReqTrack.Domain.UseCases.Core.Requirements.RequestModels;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Requirements
{
    public class GetRequirementUseCase : IGetRequirementUseCase
    {
        private readonly IRequirementRepository _requirementRepository;

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
