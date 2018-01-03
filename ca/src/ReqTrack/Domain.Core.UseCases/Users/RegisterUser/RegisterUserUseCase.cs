using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;

namespace ReqTrack.Domain.Core.UseCases.Users.RegisterUser
{
    public class RegisterUserUseCase : IUseCase<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUserRepository _userRepository;

        public RegisterUserUseCase(ISecurityGateway securityGateway, IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _userRepository = userRepository;
        }

        public void Execute(IUseCaseOutput<RegisterUserResponse> output, RegisterUserRequest request)
        {
            try
            {
                var passwordHash = UserValidationHelper.HashPassword(request.Password);
                var user = new User(Identity.BlankIdentity, request.UserName, request.DisplayName, passwordHash);

                var id = _userRepository.CreateUser(user);
                if (id == null)
                {
                    throw new Exception("Couldn't register user");
                }

                output.Accept(new RegisterUserResponse(ExecutionStatus.Success)
                {
                    GivenId = user.Id,
                    Message = $"User {user.UserName} successfuly created",
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
