using System;
using System.Linq;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase
{
    public class ViewUseCaseUseCase : IUseCase<ViewUseCaseRequest, ViewUseCaseResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUseCaseRepository _useCaseRepository;

        public ViewUseCaseUseCase(ISecurityGateway securityGateway, IUseCaseRepository useCaseRepository)
        {
            _securityGateway = securityGateway;
            _useCaseRepository = useCaseRepository;
        }

        public bool Execute(IUseCaseOutput<ViewUseCaseResponse> output, ViewUseCaseRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);
                if (rights == null || !rights.CanViewProject)
                {
                    throw new AccessViolationException("");
                }

                var useCase = _useCaseRepository.ReadUseCase(request.UseCaseId);

                return output.Accept(new ViewUseCaseResponse
                {
                    Project = new Project {Id = useCase.Project.Id, Name = useCase.Project.Name},
                    Author = new User {Id = useCase.Author.Id, Name = useCase.Author.DisplayName},
                    UseCaseId = useCase.Id,
                    Title = useCase.Title,
                    Note = useCase.Note,
                    Steps = useCase.Steps.Select(x => x.Content),
                });
            }
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse(e.ValidationErrors, $"Invalid request. {e.Message}"));
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
