using System;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Exceptions;

namespace ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser
{
    public class AuthorizeUserUseCase : IUseCase<AuthorizeUserRequest, AuthorizeUserResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUserRepository _userRepository;

        public AuthorizeUserUseCase(ISecurityGateway securityGateway, IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _userRepository = userRepository;
        }

        public void Execute(IUseCaseOutput<AuthorizeUserResponse> output, AuthorizeUserRequest request)
        {
            try
            {
                request.ValidateAndThrowOnInvalid();

                var passwordHash = UserValidationHelper.HashPassword(request.Password);
                var user = _userRepository.FindUserInfo(request.UserName, passwordHash);

                output.Accept(new AuthorizeUserResponse
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
                    Message = $"Username or password is invalid.",
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
