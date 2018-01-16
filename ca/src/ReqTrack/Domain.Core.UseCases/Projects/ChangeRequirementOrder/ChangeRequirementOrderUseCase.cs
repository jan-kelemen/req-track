using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder
{
    public class ChangeRequirementOrderUseCase 
        : IUseCase<ChangeRequirementOrderInitialRequest, ChangeRequirementOrderRequest, ChangeRequirementOrderResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public ChangeRequirementOrderUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }


        public bool Execute(IUseCaseOutput<ChangeRequirementOrderResponse> output, ChangeRequirementOrderInitialRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeRequirements)
                {
                    return output.Accept(new FailureResponse("User can't change requirements of this project."));
                }

                var project = _projectRepository.ReadProjectRequirements(request.ProjectId, Enum.Parse<RequirementType>(request.Type));

                return output.Accept(new ChangeRequirementOrderResponse
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

        public bool Execute(IUseCaseOutput<ChangeRequirementOrderResponse> output, ChangeRequirementOrderRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeRequirements)
                {
                    return output.Accept(new FailureResponse("User can't change requirements of this project."));
                }

                var type = Enum.Parse<RequirementType>(request.Type);

                var project = _projectRepository.ReadProjectRequirements(request.ProjectId, type);

                var i = 0;
                var updateList = new List<Project.Requirement>();
                foreach (var requirement in request.Requirements)
                {
                    updateList.Add(new Project.Requirement(
                        requirement.Id,
                        type,
                        requirement.Title,
                        i++)
                    );
                }

                project.ChangeRequirements(new Project.Requirement[] {}, updateList, new Project.Requirement[] { });

                if (!_projectRepository.UpdateProjectRequirements(project, type))
                {
                    output.Accept(new FailureResponse("Requirements couldn't be updated"));
                };

                return output.Accept(new ChangeRequirementOrderResponse
                {
                    Message = $"Requirements of {project.Name} successfully updated.",
                });
            }
            catch (ValidationException e)
            {
                var errors = new Dictionary<string, string> { { e.PropertyKey, e.Message } };
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
