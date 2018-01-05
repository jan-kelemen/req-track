using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Users.ChangePassword;
using ReqTrack.Domain.Core.UseCases.Users.DeleteUser;
using ReqTrack.Domain.Core.UseCases.Users.RegisterUser;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;
namespace ReqTrack.Domain.Core.UseCases.Factories
{
    public interface IUserUseCaseFactory
    {
        IUseCase<ChangeInformationInitialRequest, ChangeInformationRequest, ChangeInformationResponse> ChangeInformation { get; }

        IUseCase<ChangePasswordInitialRequest, ChangePasswordRequest, ChangePasswordResponse> ChangePassword { get; }

        IUseCase<DeleteUserRequest, DeleteUserResponse> DeleteUser { get; }

        IUseCase<RegisterUserRequest, RegisterUserResponse> RegisterUser { get; }

        IUseCase<ViewProfileRequest, ViewProfileResponse> ViewProfile { get; }

        IUseCase<AuthorizeUserRequest, AuthorizeUserResponse> AuthorizeUser { get; }
    }
}
