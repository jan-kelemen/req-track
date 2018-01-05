using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder
{
    public class ChangeRequirementOrderUseCase 
        : IUseCase<ChangeRequirementOrderInitialRequest, ChangeRequirementOrderRequest, ChangeRequirementOrderResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public ChangeRequirementOrderUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }


        public bool Execute(IUseCaseOutput<ChangeRequirementOrderResponse> output, ChangeRequirementOrderInitialRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.CanChangeRequirements)
                {
                    throw new AccessViolationException("Project doesn't exist or user has insufficient rights");
                }

                var project = _projectRepository.ReadProjectRequirements(request.ProjectId, Enum.Parse<RequirementType>(request.Type));

                return output.Accept(new ChangeRequirementOrderResponse
                {
                    Name = project.Name,
                    ProjectId = request.ProjectId,
                    Type = request.Type,
                    Requirements = project.Requirements.Select(r => new Requirement
                    {
                        Id = r.Id,
                        Title = r.Title,
                    }),
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
                    Message = $"Project not found. {e.Message}",
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

        public bool Execute(IUseCaseOutput<ChangeRequirementOrderResponse> output, ChangeRequirementOrderRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.CanChangeRequirements)
                {
                    throw new AccessViolationException("User can't change requirements of the project");
                }

                var type = Enum.Parse<RequirementType>(request.Type);

                var project = _projectRepository.ReadProjectRequirements(request.ProjectId, type);

                var i = 0;
                var updateList = new List<Project.Requirement>();
                foreach (var requirement in request.Requirements)
                {
                    updateList.Add(new Project.Requirement(
                        requirement.Id,
                        type,
                        requirement.Title,
                        i++)
                    );
                }

                project.ChangeRequirements(new Project.Requirement[] {}, updateList, new Project.Requirement[] { });

                return output.Accept(new ChangeRequirementOrderResponse
                {
                    Message = $"Requirements of {project.Name} successfully updated.",
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
                    Message = $"Project not found. {e.Message}",
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
