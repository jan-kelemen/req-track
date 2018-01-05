using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

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

        public bool Execute(IUseCaseOutput<CreateProjectResponse> output, CreateProjectRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var user = _userRepository.ReadUserInfo(request.RequestedBy);
                var project = new Project(Identity.BlankIdentity, user, request.Name, request.Description);
                var id = _projectRepository.CreateProject(project);

                if (id == null)
                {
                    throw new Exception("Couldn't create project");
                }

                return output.Accept(new CreateProjectResponse
                {
                    GivenId = id,
                    Message = $"Project {project.Name} successfuly created",
                });
            }
            catch (ValidationException e)
            {
                var errors = new Dictionary<string, string> { { e.PropertyKey, e.Message } };
                return output.Accept(new ValidationErrorResponse(errors, $"Invalid data for {e.PropertyKey}."));
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse($"Tehnical error happend. {e.Message}"));
            }
        }
    }
}
