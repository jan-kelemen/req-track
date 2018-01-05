using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.DeleteRequirement
{
    public class DeleteRequirementUseCase : IUseCase<DeleteRequirementRequest, DeleteRequirementResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IRequirementRepository _requirementRepository;

        public DeleteRequirementUseCase(
            ISecurityGateway securityGateway, 
            IRequirementRepository requirementRepository)
        {
            _securityGateway = securityGateway;
            _requirementRepository = requirementRepository;
        }

        public bool Execute(IUseCaseOutput<DeleteRequirementResponse> output, DeleteRequirementRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);

                if (!rights.CanChangeRequirements)
                {
                    return output.Accept(new FailureResponse("User can't change requiremnts of this project"));
                }

                if (!_requirementRepository.DeleteRequirement(request.RequirementId))
                {
                    return output.Accept(new FailureResponse("Requirement couldn't be deleted."));
                }

                return output.Accept(new DeleteRequirementResponse
                {
                    Message = "Requirement deleted successfully",
                });
            }
            catch (EntityNotFoundException e)
            {
                return output.Accept(new FailureResponse($"Entity not found. {e.Message}"));
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse($"Tehnical error happend. {e.Message}"));
            }
        }
    }
}
