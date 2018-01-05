using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement
{
    public class ViewRequirementUseCase : IUseCase<ViewRequirementRequest, ViewRequirementResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IRequirementRepository _requirementRepository;

        public ViewRequirementUseCase(ISecurityGateway securityGateway, IRequirementRepository requirementRepository)
        {
            _securityGateway = securityGateway;
            _requirementRepository = requirementRepository;
        }

        public bool Execute(IUseCaseOutput<ViewRequirementResponse> output, ViewRequirementRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanViewProject)
                {
                    return output.Accept(new FailureResponse("User can't view this project."));
                }

                var requirement = _requirementRepository.ReadRequirement(request.RequirementId);

                return output.Accept(new ViewRequirementResponse
                {
                    Project = new Project {Id = requirement.Project.Id, Name = requirement.Project.Name},
                    Author = new User {Id = requirement.Author.Id, Name = requirement.Author.DisplayName},
                    RequirementId = requirement.Id,
                    Title = requirement.Title,
                    Type = requirement.Type.ToString(),
                    Note = requirement.Note
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
