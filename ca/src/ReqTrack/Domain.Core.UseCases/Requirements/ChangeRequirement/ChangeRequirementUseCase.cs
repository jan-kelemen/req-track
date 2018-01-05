using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

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
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeRequirements)
                {
                    return output.Accept(new FailureResponse("User can't change requirements of this project."));
                }

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
            catch (EntityNotFoundException e)
            {
                return output.Accept(new FailureResponse($"Entity not found. {e.Message}"));
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse($"Tehnical error happend. {e.Message}"));
            }
        }

        public bool Execute(IUseCaseOutput<ChangeRequirementResponse> output, ChangeRequirementRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeRequirements)
                {
                    return output.Accept(new FailureResponse("User can't change requirements of this project."));
                }

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
            catch (ValidationException e)
            {
                var errors = new Dictionary<string, string> { { e.PropertyKey, e.Message } };
                return output.Accept(new ValidationErrorResponse(errors, $"Invalid data for {e.PropertyKey}."));
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
