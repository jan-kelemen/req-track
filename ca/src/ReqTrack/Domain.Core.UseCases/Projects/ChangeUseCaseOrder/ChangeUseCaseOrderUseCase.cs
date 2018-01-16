using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeUseCaseOrder
{
    public class ChangeUseCaseOrderUseCase 
        : IUseCase<ChangeUseCaseOrderInitialRequest, ChangeUseCaseOrderRequest, ChangeUseCaseOrderResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        public ChangeUseCaseOrderUseCase(ISecurityGateway securityGateway, IProjectRepository projectRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
        }


        public bool Execute(IUseCaseOutput<ChangeUseCaseOrderResponse> output, ChangeUseCaseOrderInitialRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeUseCases)
                {
                    return output.Accept(new FailureResponse("User can't change use cases of this project."));
                }

                var project = _projectRepository.ReadProject(request.ProjectId, false, true);

                return output.Accept(new ChangeUseCaseOrderResponse
                {
                    Name = project.Name,
                    ProjectId = request.ProjectId,
                    UseCases = project.UseCases.Select(r => new UseCase
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

        public bool Execute(IUseCaseOutput<ChangeUseCaseOrderResponse> output, ChangeUseCaseOrderRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeUseCases)
                {
                    return output.Accept(new FailureResponse("User can't change use cases of the project"));
                }

                var project = _projectRepository.ReadProject(request.ProjectId, false, true);

                var i = 0;
                var updateList = new List<Project.UseCase>();
                foreach (var useCase in request.UseCases)
                {
                    updateList.Add(new Project.UseCase(
                        useCase.Id,
                        useCase.Title,
                        i++)
                    );
                }

                project.ChangeUseCases(new Project.UseCase[] {}, updateList, new Project.UseCase[] { });

                if (!_projectRepository.UpdateProject(project, true))
                {
                    output.Accept(new FailureResponse("Use cases couldn't be updated"));
                };

                return output.Accept(new ChangeUseCaseOrderResponse
                {
                    Message = $"Use cases of {project.Name} successfully updated.",
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
