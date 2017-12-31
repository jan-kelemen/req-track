using System;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Extensions;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Exceptions;

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
                request.ValidateAndThrowOnInvalid();

                var user = _userRepository.ReadUserInfo(request.UserId);

                output.Response = new ChangeInformationResponse(ExecutionStatus.Success)
                {
                    UserId = user.Id,
                    DisplayName = user.DisplayName,
                };
            }
            catch (RequestValidationException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"User not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }

        public void Execute(IUseCaseOutput<ChangeInformationResponse> output, ChangeInformationRequest request)
        {
            try
            {
                var user = _userRepository.ReadUserInfo(request.UserId);
                user.DisplayName = request.DisplayName;

                if (!_userRepository.UpdateUserInfo(user))
                {
                    throw new Exception("Couldn't update user information");
                }

                output.Response = new ChangeInformationResponse(ExecutionStatus.Success)
                {
                    UserId = request.UserId,
                    Message = "Username changed successfully",

                };
            }
            catch (RequestValidationException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"Invalid request: {e.Message}",
                    ValidationErrors = e.ValidationErrors,
                };
            }
            catch (ValidationException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"Invalid data for {e.PropertyKey}: {e.Message}",
                };
            }
            catch (EntityNotFoundException e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"User not found: {e.Message}",
                };
            }
            catch (Exception e)
            {
                output.Response = new ChangeInformationResponse(ExecutionStatus.Failure)
                {
                    UserId = request.UserId,
                    Message = $"Tehnical error happend: {e.Message}",
                };
            }
        }
    }
}
