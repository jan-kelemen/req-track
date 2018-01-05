using System;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

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
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanViewProject)
                {
                    return output.Accept(new FailureResponse("User can't view this project."));
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
