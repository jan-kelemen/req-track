using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Requirements.RequestModels;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Requirements
{
    public class CreateRequirementUseCase : IUseCaseInputBoundary<CreateRequirementRequest, CreateRequirementResponse>
    {
        private IRequirementRepository _requirementRepository;

        public CreateRequirementUseCase(IRequirementRepository requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }

        public void Execute(IUseCaseOutputBoundary<CreateRequirementResponse> outputBoundary, CreateRequirementRequest requestModel)
        {
            requestModel.Requirement.Id = _requirementRepository.GenerateNewIdentity().ToBoundaryIdentity();
            var requirement = requestModel.Requirement.ToDomainEntity();

            var result = _requirementRepository.CreateRequirement(requirement);

            if(!result)
            {
                //TODO
            }

            outputBoundary.ResponseModel = new CreateRequirementResponse
            {
                Requirement = result.Created.ToBoundaryObject(),
            };
        }
    }
}
