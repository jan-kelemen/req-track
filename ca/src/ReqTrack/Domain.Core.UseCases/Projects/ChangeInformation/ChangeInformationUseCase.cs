using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation
{
    public class ChangeInformationUseCase 
        : IUseCase<ChangeInformationInitialRequest, ChangeInformationRequest, ChangeInformationResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public ChangeInformationUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }

        public bool Execute(IUseCaseOutput<ChangeInformationResponse> output, ChangeInformationInitialRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.IsAdministrator)
                {
                    return output.Accept(new FailureResponse("User can't change information of the project."));
                }

                var project = _projectRepository.ReadProject(request.ProjectId, false, false);

                return output.Accept(new ChangeInformationResponse
                {
                    ProjectId = project.Id,
                    Name = project.Name,
                    Description = project.Description,
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

        public bool Execute(IUseCaseOutput<ChangeInformationResponse> output, ChangeInformationRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.IsAdministrator)
                {
                    return output.Accept(new FailureResponse("User can't change information of the project."));
                }

                var project = _projectRepository.ReadProject(request.ProjectId, false, false);

                project.Name = request.Name;
                project.Description = request.Description;

                if (_projectRepository.UpdateProject(project, false))
                {
                    return output.Accept(new FailureResponse("Couldn't update the project."));
                }

                return output.Accept(new ChangeInformationResponse
                {
                    Message = $"Project {project.Name} successfully updated.",
                });
            }
            catch (ValidationException e)
            {
                var errors = new Dictionary<string, string> {{e.PropertyKey, e.Message}};
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
