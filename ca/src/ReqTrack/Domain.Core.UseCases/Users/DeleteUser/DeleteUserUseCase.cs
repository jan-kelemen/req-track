using System;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;

namespace ReqTrack.Domain.Core.UseCases.Users.DeleteUser
{
    public class DeleteUserUseCase : IUseCase<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUserRepository _userRepository;

        public DeleteUserUseCase(ISecurityGateway securityGateway, IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _userRepository = userRepository;
        }

        public void Execute(IUseCaseOutput<DeleteUserResponse> output, DeleteUserRequest request)
        {
            try
            {
                var result = _userRepository.DeleteUser(request.UserId);

                output.Response = new DeleteUserResponse(ExecutionStatus.Success)
                {
                    Message = "User deleted successfully",
                };
            }
            catch (Exception e)
            {
                output.Response = new DeleteUserResponse(ExecutionStatus.TechnicalError);
            }
        }
    }
}
