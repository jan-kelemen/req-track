using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;

namespace ReqTrack.Domain.Core.UseCases.Projects.CreateProject
{
    public class CreateProjectUseCase : IUseCase<CreateProjectRequest, CreateProjectResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        private readonly IUserRepository _userRepository;

        public CreateProjectUseCase(
            ISecurityGateway securityGateway, 
            IProjectRepository projectRepository, 
            IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public void Execute(IUseCaseOutput<CreateProjectResponse> output, CreateProjectRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var user = _userRepository.ReadUserInfo(request.RequestedBy);
                var project = new Project(Identity.BlankIdentity, user, request.Name, request.Description);
                var id = _projectRepository.CreateProject(project);

                if (id == null)
                {
                    throw new Exception("Couldn't create project");
                }

                output.Accept(new CreateProjectResponse(ExecutionStatus.Success)
                {
                    GivenId = id,
                    Message = $"Project {project.Name} successfuly created",
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
            catch (ValidationException e)
            {
                output.Accept(new ValidationErrorResponse
                {
                    Message = $"Invalid data for {e.PropertyKey}.",
                    ValidationErrors = new Dictionary<string, string>
                    {
                        { e.PropertyKey, e.Message }
                    },
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
