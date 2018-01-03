using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangePassword
{
    public class ChangePasswordUseCase
        : IUseCase<ChangePasswordInitialRequest, ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUserRepository _userRepository;

        public ChangePasswordUseCase(ISecurityGateway securityGateway, IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _userRepository = userRepository;
        }

        public void Execute(IUseCaseOutput<ChangePasswordResponse> output, ChangePasswordInitialRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var user = _userRepository.ReadUserInfo(request.UserId);
                output.Accept(new ChangePasswordResponse
                {
                    UserId = user.Id,
                    DisplayName = user.DisplayName,
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
            catch (EntityNotFoundException e)
            {
                output.Accept(new FailureResponse
                {
                    Message = $"User not found. {e.Message}",
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

        public void Execute(IUseCaseOutput<ChangePasswordResponse> output, ChangePasswordRequest request)
        {
            try
            {
                var user = _userRepository.ReadUser(request.RequestedBy, false);
                var passwordHash = UserValidationHelper.HashPassword(request.NewPassword);
                user.PasswordHash = passwordHash;

                if (_userRepository.UpdateUser(user))
                {
                    throw new Exception("Couldn't update user password");
                }

                output.Accept(new ChangePasswordResponse
                {
                    UserId = user.Id,
                    Message = "Password changed successfully",
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
            catch (EntityNotFoundException e)
            {
                output.Accept(new FailureResponse
                {
                    Message = $"User not found. {e.Message}",
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
