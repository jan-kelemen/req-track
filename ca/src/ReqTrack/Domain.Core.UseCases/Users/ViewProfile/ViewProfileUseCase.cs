﻿using System;
using System.Linq;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

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

        public bool Execute(IUseCaseOutput<ViewProfileResponse> output, ViewProfileRequest request)
        {
            try
            {
                if (!request.Validate(out var errors))
                {
                    return output.Accept(new ValidationErrorResponse(errors, "Invalid request."));
                }

                var user = _userRepository.ReadUser(request.UserId, true);

                return output.Accept(new ViewProfileResponse
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Projects = user.Projects.Select(x => new Project
                    {
                        Identifier = x.Id,
                        Name = x.Name,
                    }),
                });
            }
            catch (EntityNotFoundException e)
            {
                return output.Accept(new FailureResponse($"Entity not found. {e.Message}"));
            }
            catch (Exception e)
            {
                return output.Accept(new FailureResponse($"Tehnical error happend. {e.Message}"));
            }
        }
    }
}
