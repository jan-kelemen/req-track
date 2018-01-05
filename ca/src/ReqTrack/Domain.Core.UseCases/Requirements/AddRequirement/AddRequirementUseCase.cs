using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement
{
    public class AddRequirementUseCase : IUseCase<AddRequirementRequest, AddRequirementResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        private readonly IUserRepository _userRepository;

        private readonly IRequirementRepository _requirementRepository;

        public AddRequirementUseCase(
            ISecurityGateway securityGateway, 
            IProjectRepository projectRepository,
            IRequirementRepository requirementRepository,
            IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _requirementRepository = requirementRepository;
        }

        public bool Execute(IUseCaseOutput<AddRequirementResponse> output, AddRequirementRequest request)
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

                var user = _userRepository.ReadUserInfo(request.RequestedBy);
                var project = _projectRepository.ReadProject(request.ProjectId, false, false);
                var requirement = new Requirement(
                    Identity.BlankIdentity, 
                    project, 
                    user,
                    Enum.Parse<RequirementType>(request.Type), 
                    request.Title, 
                    request.Note);

                var id = _requirementRepository.CreateRequirement(requirement);

                if (id == null)
                {
                    throw new Exception("Couldn't create requirement");
                }

                return output.Accept(new AddRequirementResponse
                {
                    GivenId = id,
                    Message = $"Requirement successfuly created",
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
