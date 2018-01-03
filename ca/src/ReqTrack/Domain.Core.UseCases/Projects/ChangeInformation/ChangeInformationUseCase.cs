using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

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

        public void Execute(IUseCaseOutput<ChangeInformationResponse> output, ChangeInformationInitialRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.IsAdministrator)
                {
                    throw new AccessViolationException("User can't change information of the project");
                }

                var project = _projectRepository.ReadProject(request.ProjectId, false, false);

                output.Response = new ChangeInformationResponse(ExecutionStatus.Success)
                {
                    ProjectId = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                };
            }
            catch (RequestValidationException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (AccessViolationException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    Message = e.Message,
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Project not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }

        public void Execute(IUseCaseOutput<ChangeInformationResponse> output, ChangeInformationRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.IsAdministrator)
                {
                    throw new AccessViolationException("User can't change information of the project");
                }

                var project = _projectRepository.ReadProject(request.ProjectId, false, false);

                project.Name = request.Name;
                project.Description = request.Description;

                output.Response = new ChangeInformationResponse(ExecutionStatus.Success)
                {
                    Message = $"Project {project.Name} successfully updated.",
                    ProjectId = project.Id,
                };
            }
            catch (RequestValidationException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (AccessViolationException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    Message = e.Message,
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Project not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }
    }
}
