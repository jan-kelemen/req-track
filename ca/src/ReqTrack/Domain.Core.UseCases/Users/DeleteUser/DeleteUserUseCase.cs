using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Exceptions;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;

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
                if (!_userRepository.DeleteUser(request.UserId))
                {
                    throw new Exception("Couldn't delete user");
                }

                output.Response = new DeleteUserResponse(ExecutionStatus.Success)
                {
                    Message = "User deleted successfully",
                };
            }
            catch (RequestValidationException e)
            {
                output.Response = new DeleteUserResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new DeleteUserResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"User not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new DeleteUserResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }
    }
}
