﻿using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Requirements
{
    public class UpdateRequirementRequest
    {
        /// <summary>
        /// Requirement to be updated.
        /// </summary>
        public Requirement Requirement { get; set; }
    }

    public class UpdateRequirementResponse
    {
        /// <summary>
        /// Updated requirement.
        /// </summary>
        public Requirement Requirement { get; set; }
    }

    internal class UpdateRequirementUseCase : IUseCaseInputBoundary<UpdateRequirementRequest, UpdateRequirementResponse>
    {
        private IRequirementRepository _requirementRepository;

        public UpdateRequirementUseCase(IRequirementRepository requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }

        public void Execute(IUseCaseOutputBoundary<UpdateRequirementResponse> outputBoundary, UpdateRequirementRequest requestModel)
        {
            var requirementToUpdate = requestModel.Requirement.ToDomainEntity();
            var result = _requirementRepository.UpdateRequirement(requirementToUpdate);

            if (!result)
            {
                //TODO: handle update error
            }

            outputBoundary.ResponseModel = new UpdateRequirementResponse
            {
                Requirement = result.Updated.ToBoundaryObject(),
            };
        }
    }
}
