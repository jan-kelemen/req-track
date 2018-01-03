using System;
using System.Linq;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRights
{
    public class ChangeRightsUseCase
        : IUseCase<ChangeRightsInitialRequest, ChangeRightsRequest, ChangeRightsResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public ChangeRightsUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutput<ChangeRightsResponse> output, ChangeRightsInitialRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var userRights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!userRights.CanChangeProjectRights)
                {
                    throw new AccessViolationException("User can't change information of the project");
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId);

                output.Response = new ChangeRightsResponse(ExecutionStatus.Success)
                {
                    ProjectId = request.ProjectId,
                    Rights = rights.Select(r => new ProjectRights
                    {
                        UserId = r.UserId,
                        CanViewProject = r.CanViewProject,
                        CanChangeRequirements = r.CanChangeRequirements,
                        CanChangeUseCases = r.CanChangeUseCases,
                        CanChangeProjectRights = r.CanChangeProjectRights,
                        IsAdministrator = r.IsAdministrator,
                    }),
                };
            }
            catch (RequestValidationException e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (AccessViolationException e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    Message = e.Message,
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Project not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }

        public void Execute(IUseCaseOutput<ChangeRightsResponse> output, ChangeRightsRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var userRights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!userRights.CanChangeProjectRights)
                {
                    throw new AccessViolationException("User can't change information of the project");
                }

                if (request.Rights == null)
                {
                    var project = _projectRepository.ReadProject(request.ProjectId, false, false);
                    request.Rights = new[]
                    {
                        new ProjectRights
                        {
                            UserId = project.Author.Id,
                            CanViewProject = true,
                            CanChangeRequirements = true,
                            CanChangeUseCases = true,
                            CanChangeProjectRights = true,
                            IsAdministrator = true,
                        }
                    };
                }

                var result = _securityGateway.ChangeProjectRights(
                    request.ProjectId,
                    request.Rights.Select(r => new Security.ProjectRights(
                        r.UserId,
                        request.ProjectId,
                        r.CanViewProject,
                        r.CanChangeRequirements,
                        r.CanChangeUseCases,
                        r.CanChangeProjectRights,
                        r.IsAdministrator)
                    )
                );

                if (result)
                {
                    output.Response = new ChangeRightsResponse(ExecutionStatus.Success)
                    {
                        ProjectId = request.ProjectId,
                        Message = "Project rights successfully updated"
                    };
                }
                else
                {
                    output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                    {
                        ProjectId = request.ProjectId,
                        Rights = request.Rights,
                        Message = "Project rights couldn't be updated",
                    };
                }
            }
            catch (RequestValidationException e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (AccessViolationException e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    Message = e.Message,
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Project not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeRightsResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }
    }
}
