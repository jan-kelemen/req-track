using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
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

        public bool Execute(IUseCaseOutput<ChangePasswordResponse> output, ChangePasswordInitialRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var user = _userRepository.ReadUserInfo(request.UserId);
                return output.Accept(new ChangePasswordResponse
                {
                    UserId = user.Id,
                    DisplayName = user.DisplayName,
                });
            }
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse(e.ValidationErrors, $"Invalid request. {e.Message}"));
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

        public bool Execute(IUseCaseOutput<ChangePasswordResponse> output, ChangePasswordRequest request)
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

                return output.Accept(new ChangePasswordResponse
                {
                    UserId = user.Id,
                    Message = "Password changed successfully",
                });

            }
            catch (RequestValidationException e)
            {
                return output.Accept(new ValidationErrorResponse(e.ValidationErrors, $"Invalid request. {e.Message}"));
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
