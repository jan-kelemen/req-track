﻿using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

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

                if (rights == null || !rights.CanChangeUseCases)
                {
                    return output.Accept(new FailureResponse("User can't change use cases of this project."));
                }

                if (!_useCaseRepository.DeleteUseCase(request.UseCaseId))
                {
                    return output.Accept(new FailureResponse("Use case couldn't be deleted."));
                }

                return output.Accept(new DeleteUseCaseResponse
                {
                    Message = "Use case deleted successfully",
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
