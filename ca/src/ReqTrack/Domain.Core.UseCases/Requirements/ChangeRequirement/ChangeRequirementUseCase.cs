using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement
{
    public class ChangeRequirementUseCase 
        : IUseCase<ChangeRequirementInitialRequest, ChangeRequirementRequest, ChangeRequirementResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IRequirementRepository _requirementRepository;

        public ChangeRequirementUseCase(ISecurityGateway securityGateway, IRequirementRepository requirementRepository)
        {
            _securityGateway = securityGateway;
            _requirementRepository = requirementRepository;
        }

        public bool Execute(IUseCaseOutput<ChangeRequirementResponse> output, ChangeRequirementInitialRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeRequirements) { throw new AccessViolationException(""); }

                var requirement = _requirementRepository.ReadRequirement(request.RequirementId);

                return output.Accept(new ChangeRequirementResponse
                {
                    ProjectId = requirement.Project.Id,
                    RequirementId = requirement.Id,
                    Type = requirement.Type.ToString(),
                    Title = requirement.Title,
                    Note = requirement.Note,
                });
            }
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse
                {
                    Message = $"Invalid request. {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                });
            }
            catch (AccessViolationException e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"Insufficient rights. {e.Message}",
                });
            }
            catch (EntityNotFoundException e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"Entity not found. {e.Message}",
                });
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"Tehnical error happend. {e.Message}",
                });
            }
        }

        public bool Execute(IUseCaseOutput<ChangeRequirementResponse> output, ChangeRequirementRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeRequirements) { throw new AccessViolationException(""); }

                var requirement = _requirementRepository.ReadRequirement(request.RequirementId);
                requirement.Title = request.Title;
                requirement.Type = Enum.Parse<RequirementType>(request.Type);
                requirement.Note = request.Note;

                if (_requirementRepository.UpdateRequirement(requirement))
                {
                    throw new Exception("Couldn't update the requirement");
                }

                return output.Accept(new ChangeRequirementResponse
                {
                    Message = "Requirement updated successfully",
                });
            }
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse
                {
                    Message = $"Invalid request. {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                });
            }
            catch (ValidationException e)
            {
                return output.Accept(new ValidationErrorResponse
                {
                    Message = $"Invalid data for {e.PropertyKey}.",
                    ValidationErrors = new Dictionary<string, string>
                    {
                        { e.PropertyKey, e.Message }
                    },
                });
            }
            catch (AccessViolationException e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"Insufficient rights. {e.Message}",
                });
            }
            catch (EntityNotFoundException e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"Entity not found. {e.Message}",
                });
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"Tehnical error happend. {e.Message}",
                });
            }
        }
    }
}
