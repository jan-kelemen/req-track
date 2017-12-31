using System;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;

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
            output.Response = new ChangePasswordResponse(ExecutionStatus.Success)
            {
                UserId = request.UserId,
            };
        }

        public void Execute(IUseCaseOutput<ChangePasswordResponse> output, ChangePasswordRequest request)
        {
            try
            {
                var user = _userRepository.ReadUser(request.RequestedBy, false);
                var passwordHash = UserValidationHelper.HashPassword(request.NewPassword);
                user.PasswordHash = passwordHash;
                var updateResult = _userRepository.UpdateUser(user);

                output.Response = new ChangePasswordResponse(ExecutionStatus.Success)
                {
                    UserId = user.Id,
                    Message = "Password changed successfully",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangePasswordResponse(ExecutionStatus.TechnicalError);
            }
        }
    }
}
