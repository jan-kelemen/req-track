using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

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

        public void Execute(IUseCaseOutput<AddRequirementResponse> output, AddRequirementRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if(rights == null || !rights.CanChangeRequirements) { throw new AccessViolationException(""); }

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

                output.Accept(new AddRequirementResponse
                {
                    GivenId = id,
                    Message = $"Requirement successfuly created",
                });
            }
            catch (RequestValidationException e)
            {
                output.Accept(new ValidationErrorResponse
                {
                    Message = $"Invalid request. {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                });
            }
            catch (ValidationException e)
            {
                output.Accept(new ValidationErrorResponse
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
                output.Accept(new FailureResponse
                {
                    Message = $"Insufficient rights. {e.Message}",
                });
            }
            catch (EntityNotFoundException e)
            {
                output.Accept(new FailureResponse
                {
                    Message = $"Entity not found. {e.Message}",
                });
            }
            catch (Exception e)
            {
                output.Accept(new FailureResponse
                {
                    Message = $"Tehnical error happend. {e.Message}",
                });
            }
        }
    }
}
