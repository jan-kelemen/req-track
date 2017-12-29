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
                var result = _userRepository.ReadUser(request.RequestedBy, false);
                var passwordHash = UserValidationHelper.HashPassword(request.NewPassword);
                result.Read.PasswordHash = passwordHash;
                var updateResult = _userRepository.UpdateUser(result.Read, false);

                output.Response = new ChangePasswordResponse(ExecutionStatus.Success)
                {
                    UserId = updateResult.Updated.Id,
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
