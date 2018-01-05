using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Domain.Core.UseCases.UseCases.DeleteUseCase
{
    public class DeleteUseCaseUseCase : IUseCase<DeleteUseCaseRequest, DeleteUseCaseResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUseCaseRepository _useCaseRepository;

        public DeleteUseCaseUseCase(
            ISecurityGateway securityGateway, 
            IUseCaseRepository useCaseRepository)
        {
            _securityGateway = securityGateway;
            _useCaseRepository = useCaseRepository;
        }

        public bool Execute(IUseCaseOutput<DeleteUseCaseResponse> output, DeleteUseCaseRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var rights = _securityGateway.GetProjectRights(request.ProjectId, request.RequestedBy);

                if (!rights.CanChangeUseCases)
                {
                    throw new AccessViolationException("User doesn't have sufficient rights to delete the use case.");
                }

                if (!_useCaseRepository.DeleteUseCase(request.UseCaseId))
                {
                    throw new Exception("Couldn't delete useCase");
                }

                return output.Accept(new DeleteUseCaseResponse
                {
                    Message = "Use case deleted successfully",
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
    }
}
