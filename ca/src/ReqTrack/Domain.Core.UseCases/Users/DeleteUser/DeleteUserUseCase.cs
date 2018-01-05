﻿using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;

namespace ReqTrack.Domain.Core.UseCases.Users.DeleteUser
{
    public class DeleteUserUseCase : IUseCase<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUserRepository _userRepository;

        public DeleteUserUseCase(ISecurityGateway securityGateway, IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _userRepository = userRepository;
        }

        public bool Execute(IUseCaseOutput<DeleteUserResponse> output, DeleteUserRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                if (!_userRepository.DeleteUser(request.UserId))
                {
                    throw new Exception("Couldn't delete user");
                }

                return output.Accept(new DeleteUserResponse
                {
                    Message = "User deleted successfully.",
                });
            }
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse
                {
                    Message = $"Invalid request. {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                });
            }
            catch (EntityNotFoundException e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"User not found. {e.Message}",
                });
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse
                {
                    Message = $"Tehnical error happend. {e.Message}",
                });
            }
        }
    }
}
