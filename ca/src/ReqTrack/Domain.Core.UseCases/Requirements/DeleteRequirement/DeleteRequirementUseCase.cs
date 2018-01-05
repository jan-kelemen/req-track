using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

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
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);

                if (!rights.CanChangeRequirements)
                {
                    throw new AccessViolationException("User doesn't have sufficient rights to delete the requirement.");
                }

                if (!_requirementRepository.DeleteRequirement(request.RequirementId))
                {
                    throw new Exception("Couldn't delete requirement");
                }

                return output.Accept(new DeleteRequirementResponse
                {
                    Message = "Requirement deleted successfully",
                });
            }
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse(e.ValidationErrors, $"Invalid request. {e.Message}"));
            }
            catch (AccessViolationException e)
            {
                return output.Accept(new FailureResponse($"Insufficient rights. {e.Message}"));
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
