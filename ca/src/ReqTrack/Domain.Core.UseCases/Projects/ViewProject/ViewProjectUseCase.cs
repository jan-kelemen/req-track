﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

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

        public bool Execute(IUseCaseOutput<ViewProjectResponse> output, ViewProjectRequest request)
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

                var project = _projectRepository.ReadProject(request.ProjectId, request.ShowRequirements, request.ShowUseCases);

                return output.Accept(new ViewProjectResponse
                {
                    Name = project.Name,
                    Description = project.Description,
                    ProjectId = request.ProjectId,
                    Author = new User
                    {
                        Id = project.Author.Id,
                        Name = project.Author.DisplayName,
                    },
                    Rights = new ProjectRights
                    {
                        UserId = request.RequestedBy,
                        CanViewProject = rights.CanViewProject,
                        CanChangeRequirements = rights.CanChangeRequirements,
                        CanChangeUseCases = rights.CanChangeUseCases,
                        CanChangeProjectRights = rights.CanChangeProjectRights,
                        IsAdministrator = rights.IsAdministrator,
                    },
                    Requirements = project.Requirements?.Select(r => new Requirement
                    {
                        Id = r.Id,
                        Type = r.Type.ToString(),
                        Title = r.Title,
                    }) ?? new List<Requirement>(),
                    UseCases = project.UseCases?.Select(r => new UseCase
                    {
                        Id = r.Id,
                        Title = r.Title,
                    }) ?? new List<UseCase>(),
                    ShowRequirements = request.ShowRequirements,
                    ShowUseCases = request.ShowUseCases,
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
