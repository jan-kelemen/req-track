﻿using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

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
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanViewProject)
                {
                    throw new AccessViolationException("");
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
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse(e.ValidationErrors, $"Invalid request. {e.Message}"));
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