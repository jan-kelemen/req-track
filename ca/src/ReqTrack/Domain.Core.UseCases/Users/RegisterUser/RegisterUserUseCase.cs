using System;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;

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

                var result = _userRepository.CreateUser(user);

                output.Response =  new RegisterUserResponse(ExecutionStatus.Success)
                {
                    GivenId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,

                    Message = $"User {user.UserName} successfuly created",
                };
            }
            catch (Exception e)
            {
                //TODO: implement proper exception handling once exception model is defined.
                output.Response = new RegisterUserResponse(ExecutionStatus.TechnicalError)
                {
                    Message = "Technical error occured",
                };
            }
        }
    }
}
