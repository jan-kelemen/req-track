using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
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

        public bool Execute(IUseCaseOutput<ChangeRightsResponse> output, ChangeRightsInitialRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var userRights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!userRights.CanChangeProjectRights)
                {
                    throw new AccessViolationException("User can't change information of the project");
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId);

                var project = _projectRepository.ReadProject(request.ProjectId, false, false);

                return output.Accept(new ChangeRightsResponse
                {
                    ProjectId = request.ProjectId,
                    Name =  project.Name,
                    Rights = rights.Select(r => new ProjectRights
                    {
                        UserId = r.UserId,
                        CanViewProject = r.CanViewProject,
                        CanChangeRequirements = r.CanChangeRequirements,
                        CanChangeUseCases = r.CanChangeUseCases,
                        CanChangeProjectRights = r.CanChangeProjectRights,
                        IsAdministrator = r.IsAdministrator,
                    }),
                });
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

        public bool Execute(IUseCaseOutput<ChangeRightsResponse> output, ChangeRightsRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

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

                if (!result)
                {
                    throw new Exception("Project rights couldn't be updated.");
                }

                return output.Accept(new ChangeRightsResponse
                {
                    ProjectId = request.ProjectId,
                    Message = "Project rights successfully updated"
                });
            }
            catch (ValidationException e)
            {
                var errors = new Dictionary<string, string> { { e.PropertyKey, e.Message } };
                return output.Accept(new ValidationErrorResponse(errors, $"Invalid data for {e.PropertyKey}."));
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
