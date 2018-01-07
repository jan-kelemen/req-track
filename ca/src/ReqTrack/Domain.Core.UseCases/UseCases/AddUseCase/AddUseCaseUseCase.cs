using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase
{
    public class AddUseCaseUseCase : IUseCase<AddUseCaseInitialRequest, AddUseCaseRequest, AddUseCaseResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IProjectRepository _projectRepository;

        private readonly IUserRepository _userRepository;

        private readonly IUseCaseRepository _useCaseRepository;

        public AddUseCaseUseCase(
            ISecurityGateway securityGateway, 
            IProjectRepository projectRepository,
            IUseCaseRepository useCaseRepository,
            IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _useCaseRepository = useCaseRepository;
        }

        public bool Execute(IUseCaseOutput<AddUseCaseResponse> output, AddUseCaseInitialRequest request)
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
                    return output.Accept(new FailureResponse("User can't change use cases of this. project"));
                }

                var user = _userRepository.ReadUserInfo(request.RequestedBy);
                var project = _projectRepository.ReadProject(request.ProjectId, false, false);

                return output.Accept(new AddUseCaseResponse
                {
                    ProjectId = project.Id,
                    ProjectName = project.Name,
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

        public bool Execute(IUseCaseOutput<AddUseCaseResponse> output, AddUseCaseRequest request)
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
                    return output.Accept(new FailureResponse("User can't change use cases of this. project"));
                }

                var user = _userRepository.ReadUserInfo(request.RequestedBy);
                var project = _projectRepository.ReadProject(request.ProjectId, false, false);

                var i = 0;
                var steps = new List<UseCase.UseCaseStep>();
                foreach (var requestStep in request.Steps)
                {
                    steps.Add(new UseCase.UseCaseStep { Content = requestStep, OrderMarker = i++ });
                }

                var useCase = new UseCase(
                    Identity.BlankIdentity, 
                    project, 
                    user,
                    request.Title, 
                    request.Note,
                    steps);

                var id = _useCaseRepository.CreateUseCase(useCase);
                if (id == null)
                {
                    return output.Accept(new FailureResponse("Use case couldn't be created."));
                }

                return output.Accept(new AddUseCaseResponse
                {
                    GivenId = id,
                    ProjectId = project.Id,
                    Message = "Use case successfuly created",
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
