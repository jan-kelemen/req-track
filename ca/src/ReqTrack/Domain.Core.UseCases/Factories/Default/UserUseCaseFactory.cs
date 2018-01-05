using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Users.ChangePassword;
using ReqTrack.Domain.Core.UseCases.Users.DeleteUser;
using ReqTrack.Domain.Core.UseCases.Users.RegisterUser;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;
namespace ReqTrack.Domain.Core.UseCases.Factories.Default
{
    internal class UserUseCaseFactory : IUserUseCaseFactory
    {
        internal UserUseCaseFactory(ISecurityGateway securityGateway, IRepositoryFactory repositoryFactory)
        {
            ChangeInformation = new ChangeInformationUseCase(securityGateway, repositoryFactory.UserRepository);
            ChangePassword = new ChangePasswordUseCase(securityGateway, repositoryFactory.UserRepository);
            DeleteUser = new DeleteUserUseCase(securityGateway, repositoryFactory.UserRepository);
            RegisterUser = new RegisterUserUseCase(securityGateway, repositoryFactory.UserRepository);
            ViewProfile = new ViewProfileUseCase(securityGateway, repositoryFactory.UserRepository);
            AuthorizeUser = new AuthorizeUserUseCase(securityGateway, repositoryFactory.UserRepository);
        }

        public IUseCase<ChangeInformationInitialRequest, ChangeInformationRequest, ChangeInformationResponse> ChangeInformation { get; }

        public IUseCase<ChangePasswordInitialRequest, ChangePasswordRequest, ChangePasswordResponse> ChangePassword { get; }

        public IUseCase<DeleteUserRequest, DeleteUserResponse> DeleteUser { get; }

        public IUseCase<RegisterUserRequest, RegisterUserResponse> RegisterUser { get; }

        public IUseCase<ViewProfileRequest, ViewProfileResponse> ViewProfile { get; }

        public IUseCase<AuthorizeUserRequest, AuthorizeUserResponse> AuthorizeUser { get; }
    }
}
