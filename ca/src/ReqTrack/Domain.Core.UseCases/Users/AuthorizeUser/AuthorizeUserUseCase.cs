using System;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

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

        public bool Execute(IUseCaseOutput<AuthorizeUserResponse> output, AuthorizeUserRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var passwordHash = UserValidationHelper.HashPassword(request.Password);
                var user = _userRepository.FindUserInfo(request.UserName, passwordHash);

                return output.Accept(new AuthorizeUserResponse
                {
                    
                    UserId = user.Id,
                    DisplayName = user.DisplayName,
                });
            }
            catch (EntityNotFoundException)
            {
                return output.Accept(new FailureResponse($"Username or password is invalid."));
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse($"Tehnical error happend. {e.Message}"));
            }
        }
    }
}
