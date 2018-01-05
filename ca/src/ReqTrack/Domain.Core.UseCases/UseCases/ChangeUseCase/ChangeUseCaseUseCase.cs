using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase
{
    public class ChangeUseCaseUseCase 
        : IUseCase<ChangeUseCaseInitialRequest, ChangeUseCaseRequest, ChangeUseCaseResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUseCaseRepository _useCaseRepository;

        public ChangeUseCaseUseCase(ISecurityGateway securityGateway, IUseCaseRepository useCaseRepository)
        {
            _securityGateway = securityGateway;
            _useCaseRepository = useCaseRepository;
        }

        public bool Execute(IUseCaseOutput<ChangeUseCaseResponse> output, ChangeUseCaseInitialRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeUseCases) { throw new AccessViolationException(""); }

                var useCase = _useCaseRepository.ReadUseCase(request.UseCaseId);

                return output.Accept(new ChangeUseCaseResponse
                {
                    ProjectId = useCase.Project.Id,
                    UseCaseId = useCase.Id,
                    Title = useCase.Title,
                    Note = useCase.Note,
                    Steps = useCase.Steps.Select(x => x.Content),
                });
            }
            catch (AccessViolationException e)
            {
                return output.Accept(new FailureResponse($"Insufficient rights. {e.Message}"));
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

        public bool Execute(IUseCaseOutput<ChangeUseCaseResponse> output, ChangeUseCaseRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanChangeUseCases) { throw new AccessViolationException(""); }

                var useCase = _useCaseRepository.ReadUseCase(request.UseCaseId);


                var i = 0;
                var steps = new List<UseCase.UseCaseStep>();
                foreach (var requestStep in request.Steps)
                {
                    steps.Add(new UseCase.UseCaseStep { Content = requestStep, OrderMarker = i++ });
                }

                useCase.Title = request.Title;
                useCase.Note = request.Note;
                useCase.Steps = steps;

                if (_useCaseRepository.UpdateUseCase(useCase))
                {
                    throw new Exception("Couldn't update the use case");
                }

                return output.Accept(new ChangeUseCaseResponse
                {
                    Message = "Use case updated successfully",
                });
            }
            catch (ValidationException e)
            {
                var errors = new Dictionary<string, string> { { e.PropertyKey, e.Message } };
                return output.Accept(new ValidationErrorResponse(errors, $"Invalid data for {e.PropertyKey}."));
            }
            catch (AccessViolationException e)
            {
                return output.Accept(new FailureResponse($"Insufficient rights. {e.Message}"));
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
