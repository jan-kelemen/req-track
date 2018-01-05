using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
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

        public bool Execute(IUseCaseOutput<RegisterUserResponse> output, RegisterUserRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var passwordHash = UserValidationHelper.HashPassword(request.Password);
                var user = new User(Identity.BlankIdentity, request.UserName, request.DisplayName, passwordHash);

                var id = _userRepository.CreateUser(user);
                if (id == null)
                {
                    throw new Exception("Couldn't register user");
                }

                return output.Accept(new RegisterUserResponse
                {
                    GivenId = user.Id,
                    Message = $"User {user.UserName} successfuly created",
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
            catch (Exception e)
            {
                return output.Accept(new FailureResponse($"Tehnical error happend. {e.Message}"));
            }
        }
    }
}
