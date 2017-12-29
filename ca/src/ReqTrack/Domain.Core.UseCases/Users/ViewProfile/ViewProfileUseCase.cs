using System;
using System.Linq;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;

namespace ReqTrack.Domain.Core.UseCases.Users.ViewProfile
{
    public class ViewProfileUseCase : IUseCase<ViewProfileRequest, ViewProfileResponse>
    {
        private readonly ISecurityGateway _securityGateway;

        private readonly IUserRepository _userRepository;

        public ViewProfileUseCase(ISecurityGateway securityGateway, IUserRepository userRepository)
        {
            _securityGateway = securityGateway;
            _userRepository = userRepository;
        }

        public void Execute(IUseCaseOutput<ViewProfileResponse> output, ViewProfileRequest request)
        {
            try
            {
                var user = _userRepository.ReadUser(request.UserId, true);

                output.Response = new ViewProfileResponse(ExecutionStatus.Success)
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Projects = user.Projects.Select(x => new ViewProfileResponse.Project()
                    {
                        Identifier = x.Id,
                        Name = x.Name,
                    }),
                };
            }
            catch (Exception e)
            {
                output.Response = new ViewProfileResponse(ExecutionStatus.TechnicalError);
            }
        }
    }
}
