﻿using System;
using System.Linq;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = System.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.Projects.ViewProject
{
    public class ViewProjectUseCase : IUseCase<ViewProjectRequest, ViewProjectResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public ViewProjectUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutput<ViewProjectResponse> output, ViewProjectRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.CanViewProject)
                {
                    throw new AccessViolationException("Project doesn't exist or user has insufficient rights");
                }

                var project = _projectRepository.ReadProject(request.ProjectId, true, true);

                output.Accept(new ViewProjectResponse(ExecutionStatus.Success)
                {
                    Name = project.Name,
                    Description = project.Description,
                    ProjectId = request.ProjectId,
                    Rights = new ProjectRights
                    {
                        UserId = request.RequestedBy,
                        CanViewProject = rights.CanViewProject,
                        CanChangeRequirements = rights.CanChangeRequirements,
                        CanChangeUseCases = rights.CanChangeUseCases,
                        CanChangeProjectRights = rights.CanChangeProjectRights,
                        IsAdministrator = rights.IsAdministrator,
                    },
                    Requirements = project.Requirements.Select(r => new Requirement
                    {
                        Id = r.Id,
                        Title = r.Title,
                    }),
                    UseCases = project.UseCases.Select(r => new UseCase
                    {
                        Id = r.Id,
                        Title = r.Title,
                    }),
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
                    Message = $"Project not found. {e.Message}",
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
