using System;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.Projects.ViewProjectRequirements
{
    public class ViewProjectRequirementsUseCase : IUseCase<ViewProjectRequirementsRequest, ViewProjectRequirementsResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public ViewProjectRequirementsUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }

        public bool Execute(IUseCaseOutput<ViewProjectRequirementsResponse> output, ViewProjectRequirementsRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (!rights.CanViewProject)
                {
                    throw new AccessViolationException("Project doesn't exist or user has insufficient rights");
                }

                var project = _projectRepository.ReadProjectRequirements(request.ProjectId, Enum.Parse<RequirementType>(request.Type));

                return output.Accept(new ViewProjectRequirementsResponse
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
    }
}
