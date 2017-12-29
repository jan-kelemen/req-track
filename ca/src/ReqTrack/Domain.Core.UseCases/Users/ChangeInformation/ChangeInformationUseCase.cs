using System;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangeInformation
{
    public class ChangeInformationUseCase 
        : IUseCase<ChangeInformationInitialRequest, ChangeInformationRequest, ChangeInformationResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUserRepository _userRepository;

        public ChangeInformationUseCase(ISecurityGateway securityGateway, IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _userRepository = userRepository;
        }

        public void Execute(IUseCaseOutput<ChangeInformationResponse> output, ChangeInformationInitialRequest request)
        {
            try
            {
                var user = _userRepository.ReadUserInfo(request.UserId);

                output.Response = new ChangeInformationResponse(ExecutionStatus.Success)
                {
                    UserId = user.Id,
                    DisplayName = user.DisplayName,
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.TechnicalError);
            }
        }

        public void Execute(IUseCaseOutput<ChangeInformationResponse> output, ChangeInformationRequest request)
        {
            try
            {
                var user = _userRepository.ReadUserInfo(request.UserId);
                user.DisplayName = request.DisplayName;

                var updateResult = _userRepository.UpdateUserInfo(user);

                output.Response = new ChangeInformationResponse(ExecutionStatus.Success)
                {
                    Message = "Username changed successfully",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.TechnicalError);
            }
        }
    }
}
