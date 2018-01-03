using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.Projects.DeleteProject
{
    public class DeleteProjectUseCase : IUseCase<DeleteProjectRequest, DeleteProjectResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public DeleteProjectUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutput<DeleteProjectResponse> output, DeleteProjectRequest request)
        {
            try
            {
                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);

                if (!rights.IsAdministrator)
                {
                    throw new AccessViolationException("User doesn't have sufficient rights to delete the project.");
                }

                if (!_projectRepository.DeleteProject(request.ProjectId))
                {
                    throw new Exception("Couldn't delete project");
                }

                output.Response = new DeleteProjectResponse(ExecutionStatus.Success)
                {
                    Message = "Project deleted successfully",
                };
            }
            catch (RequestValidationException e)
            {
                output.Response = new DeleteProjectResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (AccessViolationException e)
            {
                output.Response = new DeleteProjectResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = e.Message,
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new DeleteProjectResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Project not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new DeleteProjectResponse(ExecutionStatus.Failure)
                {
                    ProjectId = request.ProjectId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }
    }
}
