using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Users.ChangePassword;
using ReqTrack.Domain.Core.UseCases.Users.DeleteUser;
using ReqTrack.Domain.Core.UseCases.Users.RegisterUser;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;

namespace ReqTrack.Domain.Core.UseCases.Factories.Default
{
    internal class UserUseCaseFactory : IUserUseCaseFactory
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IRepositoryFactory _repositoryFactory;

        internal UserUseCaseFactory(ISecurityGateway securityGateway, IRepositoryFactory repositoryFactory)
        {
            _securityGateway = securityGateway;
            _repositoryFactory = repositoryFactory;
        }

        public IUseCase<ChangeInformationInitialRequest, ChangeInformationRequest, ChangeInformationResponse>
            ChangeInformation => new ChangeInformationUseCase(_securityGateway, _repositoryFactory.UserRepository);

        public IUseCase<ChangePasswordInitialRequest, ChangePasswordRequest, ChangePasswordResponse> 
            ChangePassword => new ChangePasswordUseCase(_securityGateway, _repositoryFactory.UserRepository);

        public IUseCase<DeleteUserRequest, DeleteUserResponse> DeleteUser => 
            new DeleteUserUseCase(_securityGateway, _repositoryFactory.UserRepository);

        public IUseCase<RegisterUserRequest, RegisterUserResponse> RegisterUser => 
            new RegisterUserUseCase(_securityGateway, _repositoryFactory.UserRepository);

        public IUseCase<ViewProfileRequest, ViewProfileResponse> ViewProfile => 
            new ViewProfileUseCase(_securityGateway, _repositoryFactory.UserRepository);
    }
}
